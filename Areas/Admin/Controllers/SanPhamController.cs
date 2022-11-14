using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanHangLuuNiem.Models;
using QLBanHangLuuNiem.Models.ViewModels;
using System.IO;

using PagedList;
using PagedList.Mvc;

namespace QLBanHangLuuNiem.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
        private QLShopLuuNiemEntities7 db = new QLShopLuuNiemEntities7();
        // GET: Phim
        private List<SanPham> LaySanPham(int count)
        {
            return db.SanPhams.OrderByDescending(s => s.NamSanXuat).Take(count).ToList();
        }
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            /*List<Phim> dsp = db.Phims.ToList();*/
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var sanphammoi = (from l in db.SanPhams
                              select l).OrderByDescending(s => s.NamSanXuat);
            //var phimmoi = Layphimmoi(3);
            return View(sanphammoi.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Create()
        {
            //Lấy ra ds loại sp
            SanPhamViewModels pModel = new SanPhamViewModels();
            pModel.ListPhanLoai = db.PhanLoais.ToList();
            return View(pModel);
        }
        [HttpPost]
        public ActionResult Create(SanPham p, HttpPostedFileBase fileUpload)
        {
            //lấy tên file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Tạo đường dẫn lưu file
            var filePath = Path.Combine(Server.MapPath("~/Content/image"), fileName);
            //Lưu ảnh xuống thư mục img
            fileUpload.SaveAs(filePath);
            //kt có ảnh chưa
            if (!System.IO.File.Exists(filePath))
                fileUpload.SaveAs(filePath);


            SanPham SanPhamM = new SanPham();
            SanPhamM.TenSP = p.TenSP;
            SanPhamM.QuocGia = p.QuocGia;
            SanPhamM.DonGia = p.DonGia;
            SanPhamM.DonViTinh = p.DonViTinh;
            SanPhamM.NamSanXuat = p.NamSanXuat;
            SanPhamM.MaPhanLoai = p.MaPhanLoai;
            //lưu đường dẫn vào database
            SanPhamM.HinhSP = "Content/image/" + fileName;
            //sanPhamMoi.HinhSP = sp.HinhSP;
            db.SanPhams.Add(SanPhamM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)//truyền id
        {
            SanPham p = db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault();
            ViewBag.MaPhanLoai = new SelectList
                (db.PhanLoais.ToList().
                OrderBy(s => s.TenPhanLoai), "MaPhanLoai", "TenPhanLoai");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            //lấy tên file
            var fileName = Path.GetFileName(fileUpload.FileName);
            //Tạo đường dẫn lưu file
            var filePath = Path.Combine(Server.MapPath("~/Content/image"), fileName);
            //Lưu ảnh xuống thư mục img
            fileUpload.SaveAs(filePath);
            //kt có ảnh chưa
            if (!System.IO.File.Exists(filePath))
                fileUpload.SaveAs(filePath);


            SanPham p = db.SanPhams.Where(s => s.MaSP == sanpham.MaSP).FirstOrDefault();
            p.TenSP = sanpham.TenSP;
            p.QuocGia = sanpham.QuocGia;
            p.DonGia = sanpham.DonGia;
            p.DonViTinh = sanpham.DonViTinh;
            p.MaPhanLoai = sanpham.MaPhanLoai;
            p.NamSanXuat = sanpham.NamSanXuat;
            p.HinhSP = "Content/image/" + fileName; ;

            /* db.SanPhams.Add(sp);*///Thêm sp
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            SanPham p = db.SanPhams.FirstOrDefault(s => s.MaSP == id);
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            SanPham p = db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault();
            ViewBag.MaLoaiPhim = new SelectList
               (db.PhanLoais.ToList().
                OrderBy(s => s.TenPhanLoai), "MaPhanLoai", "TenPhanLoai");
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(SanPham sanpham, int id)
        {
            SanPham p = db.SanPhams.Single(s => s.MaSP == id);
            db.SanPhams.Remove(p);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Balo(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Balo, Túi, Ví").ToList();
            //int pageSize = 6;
            //int pageNum = (page ?? 1);
            //var sanphammoi = (from l in db.SanPhams
            //                  select l).OrderByDescending(s => s.NamSanXuat);
            ////var phimmoi = Layphimmoi(3);
            //return View(sanphammoi.ToPagedList(pageNum, pageSize));
            return View(p);
        }
        public ActionResult DongHo(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Đồng Hồ").ToList();

            return View(p);
        }
        public ActionResult MatKinh(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Mắt Kính").ToList();

            return View(p);
        }
        public ActionResult LamDep(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Phụ Kiện Làm Đẹp").ToList();

            return View(p);
        }
        public ActionResult MocKhoa(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Móc Khóa").ToList();

            return View(p);
        }
        public ActionResult VanPhongPham(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Văn Phòng Phẩm").ToList();

            return View(p);
        }
        public ActionResult TrangSuc(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Trang Sức").ToList();

            return View(p);
        }
        public ActionResult DuLich(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Phụ Kiện Du Lịch").ToList();

            return View(p);
        }
        public ActionResult Idol(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Bộ Sưu Tầm Idol").ToList();

            return View(p);
        }
        public ActionResult TrungBay(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Trưng Bày").ToList();

            return View(p);
        }
        public ActionResult GiaDung(int? page)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.PhanLoai.TenPhanLoai == "Gia Dụng").ToList();

            return View(p);
        }
        public ActionResult SearchByName(string name)
        {
            List<SanPham> p = db.SanPhams.Where(s => s.TenSP.Contains(name)).ToList();

            ViewBag.keyword = name;

            return View(p);
        }
        public ActionResult XemCTHD(int? page)
        { 
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var cthd = (from l in db.HoaDons
                              select l).OrderByDescending(s => s.MaHD);

           
            //if (db.HoaDons.FirstOrDefault(s => s.DaThanhToan.Equals())
            //Console.Write("Chưa thanh toán");

            return View(cthd.ToPagedList(pageNum, pageSize));
        }
        public ActionResult CTHD(int id)
        {
            CTHD CT = db.CTHDs.FirstOrDefault(s => s.MaHD == id);
            return View(CT);
        }
        public ActionResult SuaHD(int id)
        {
            HoaDon p = db.HoaDons.Where(s => s.MaHD == id).FirstOrDefault();
          
            ViewBag.TinhTrangGiaoHang = new SelectList
                (db.GiaoHangs.ToList().OrderBy(s => s.TinhTrangGiaoHang), "TinhTrangGiaoHang", "GiaoHangTT") ;
            ViewBag.DaThanhToan = new SelectList
              (db.ThanhToans.ToList().OrderBy(s => s.DaThanhToan), "DaThanhToan", "TinhTrang");
            return View(p);
        }
        [HttpPost]
        public ActionResult SuaHD(HoaDon Hd)
        {


            HoaDon p = db.HoaDons.Where(s => s.MaHD == Hd.MaHD).FirstOrDefault();
            p.DaThanhToan = Hd.DaThanhToan;
            p.TinhTrangGiaoHang = Hd.TinhTrangGiaoHang;
            p.NgayDat = Hd.NgayDat;
            p.NgayGiao = Hd.NgayGiao;
            //p.MaKH = Hd.MaKH;
            /* db.SanPhams.Add(sp);*///Thêm sp
            db.SaveChanges();
            return RedirectToAction("XemCTHD");
        }

    }
}
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

namespace QLBanHangLuuNiem.Controllers
{
    public class SPClientController : Controller
    {
        // GET: SPClient
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
        public ActionResult Details(int id)
        {
            SanPham p = db.SanPhams.FirstOrDefault(s => s.MaSP == id);
            return View(p);
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
    }
}
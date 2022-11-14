using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanHangLuuNiem.Models;

namespace QLBanHangLuuNiem.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private QLShopLuuNiemEntities7 db = new QLShopLuuNiemEntities7();
        public List<Giohang> Index()
        {

            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                //gio hang chưa tồn tại thì khởi tạo giỏ hàng rỗng
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
            //return View(lstGiohang);
        }
        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            //SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMaSP);
            //if (sp == null)
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}
            //List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;

            List<Giohang> lstGiohang = Index();
            Giohang sanpham = lstGiohang.Find(n => n.iMaSP == iMaSP);

            if (sanpham == null)
            {
                sanpham = new Giohang(iMaSP);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
                //return Redirect("GioHang");
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
                //return Redirect("GioHang");
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Index();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "SPClient");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGiohang(int iMaSP)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            //Kiem tra san pham da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Neu ton tai thi cho sua so luong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMaSP == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "SPClient");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoatatcaGiohang()
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            lstGiohang.Clear();
            return RedirectToAction("Index", "SanPham");
        }

        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            //Kiem tra san pham da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            //Neu ton tai thi cho sua so luong
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult Dathang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "SPClient");
            }
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Index();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don Hang
            HoaDon ddh = new HoaDon();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<Giohang> gh = Index();
            var k = 0;
            var so = db.CTHDs.Count();
            for(int i=so; i>=0; i++)
            {
                var ktr = db.CTHDs.SingleOrDefault(s => s.MaHD == k);
                if (ktr == null)
                {
                    ddh.MaKH = kh.MaKH;
                    ddh.NgayDat = DateTime.Now;
                    var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
                    ddh.NgayGiao = DateTime.Parse(ngaygiao);
                    ddh.TinhTrangGiaoHang = false;
                    ddh.DaThanhToan = false;
                    ddh.MaHD = k;
                 
                    db.HoaDons.Add(ddh);
                    db.SaveChanges();
                    AddHD(k);
                    //Them chi tiet don hang

                    Session["GioHang"] = null;

                    return RedirectToAction("Xacnhandonhang", "Giohang");
                }
               k = i;

            }
            return RedirectToAction("Xacnhandonhang", "Giohang");


            //return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        //tách
        public void AddHD(int k) {
            List<Giohang> gh = Index();
            foreach (var item in gh)
            {
                CTHD ctdh = new CTHD();
                ctdh.MaHD = k;
                ctdh.MaSP = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGiaBan = (decimal)item.iDonGia;
                db.CTHDs.Add(ctdh);
            }


            db.SaveChanges();
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }


    }
}
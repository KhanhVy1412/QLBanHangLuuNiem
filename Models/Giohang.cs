using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBanHangLuuNiem.Models;

namespace QLBanHangLuuNiem.Models
{
   
    public class Giohang
    {
        private QLShopLuuNiemEntities7 db = new QLShopLuuNiemEntities7();
        public int iMaSP { get; set; }
        public String sTenSP { get; set; }
        public double iDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get
            {
                return iSoLuong * iDonGia;
            }
        }
        public Giohang(int MaSP)
        {
            iMaSP = MaSP;
            SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
            sTenSP = sp.TenSP;
            iDonGia = double.Parse(sp.DonGia.ToString());
            iSoLuong = 1;

        }
    }
}
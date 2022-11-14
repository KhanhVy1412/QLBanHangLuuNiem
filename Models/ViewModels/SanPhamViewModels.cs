using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanHangLuuNiem.Models.ViewModels
{
    public class SanPhamViewModels
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<int> MaPhanLoai { get; set; }
        public string NamSanXuat { get; set; }
        public string QuocGia { get; set; }
        public string HinhSP { get; set; }


        //public virtual ICollection<CTHD> CTHDs { get; set; }
        public IEnumerable<PhanLoai> ListPhanLoai { get; set; }
    }
    }
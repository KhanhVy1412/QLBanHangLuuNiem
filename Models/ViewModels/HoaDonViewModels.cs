using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanHangLuuNiem.Models.ViewModels
{
    public class HoaDonViewModels
    {

        public int MaHD { get; set; }
        public Nullable<bool> DaThanhToan { get; set; }
        public Nullable<bool> TinhTrangGiaoHang { get; set; }
        public Nullable<System.DateTime> NgayDat { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        public Nullable<int> MaKH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual ThanhToan ThanhToan { get; set; }
        public IEnumerable<ThanhToan> LThanhToan { get; set; }
        public IEnumerable<GiaoHang> LGiaoHang { get; set; }
    }
}
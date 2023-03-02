using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

#nullable disable

namespace WebApp.Models
{
    public partial class Khachhang
    {
        public string Makh { get; set; }
        [Display(Name = "Tên Khách Hàng")]
        [Required(ErrorMessage = "Vui lòng nhập Tên")]
        public string Tenkh { get; set; }
        [Display(Name = "địa chỉ khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Diachi { get; set; }
        [Display(Name = "Sđt khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập sđt")]
        public string Dt { get; set; }
        public string Email { get; set; }
        public string Madichvu { get; set; }
        public int? Maduan { get; set; }
        public string Ghichu { get; set; }

        public virtual Dichvu MadichvuNavigation { get; set; }
        public virtual Duan MaduanNavigation { get; set; }
    }
}

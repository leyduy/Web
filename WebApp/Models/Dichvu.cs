using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class Dichvu
    {
        public Dichvu()
        {
            Khachhangs = new HashSet<Khachhang>();
        }

        public string Madichvu { get; set; }
        public string Loaidichvu { get; set; }

        public virtual ICollection<Khachhang> Khachhangs { get; set; }
    }
}

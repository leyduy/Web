using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Models
{
    public partial class Duan
    {
        public Duan()
        {
            Khachhangs = new HashSet<Khachhang>();
        }

        public int Maduan { get; set; }
        public string Loaiduan { get; set; }

        public virtual ICollection<Khachhang> Khachhangs { get; set; }
    }
}

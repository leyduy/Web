using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly DACNContext _context;
        public INotyfService _notyfService { get; }
        private readonly ILogger<HomeController> _logger;



        public HomeController(DACNContext context, INotyfService notyfService)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [Route("gioithieu.html", Name = "Gioithieu")]
        public IActionResult GioiThieu()
        {
            return View();
        }
        [Route("DDichvu.html", Name = "DichVu")]
        public IActionResult DichVu()
        {
            return View();
        }
        [Route("Chitietdichvu.html", Name = "ChiTietDichVu")]
        public IActionResult ChiTietDichVu()
        {
            return View();
        }

        // GET: Products
        [Route("Sanpham.html", Name = "SanPham")]
        public IActionResult sanpham()
        {
            return View();
        }
        
        public IActionResult Lienhe()
        {

            ViewData["Madichvu"] = new SelectList(_context.Dichvus, "Madichvu", "Madichvu");
            ViewData["Maduan"] = new SelectList(_context.Duans, "Maduan", "Maduan");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Lienhe([Bind("Makh,Tenkh,Diachi,Dt,Email,Madichvu,Maduan,Ghichu")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachhang);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["Madichvu"] = new SelectList(_context.Dichvus, "Madichvu", "Madichvu", khachhang.Madichvu);
            ViewData["Maduan"] = new SelectList(_context.Duans, "Maduan", "Maduan", khachhang.Maduan);
            return View(khachhang);
        }


    }
}

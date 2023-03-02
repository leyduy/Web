using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize]
    public class KhachhangsController : Controller
    {
        private readonly DACNContext _context;
        private readonly INotyfService _notyfService;
        private INotyfService notyfService;

        public KhachhangsController(DACNContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Khachhangs
       
        public async Task<IActionResult> Index()
        {
            ViewData["loaiduan"] = new SelectList(_context.Duans, "Maduan", "Maduan");
            ViewData["loaidichvu"] = new SelectList(_context.Duans, "Madichvu", "Loaidichvu");

            var dACNContext = _context.Khachhangs.Include(k => k.MadichvuNavigation).Include(k => k.MaduanNavigation);
            return View(await dACNContext.ToListAsync());
        }

        // GET: Admin/Khachhangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .Include(k => k.MadichvuNavigation)
                .Include(k => k.MaduanNavigation)
                .FirstOrDefaultAsync(m => m.Makh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        

        // GET: Admin/Khachhangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            ViewData["Madichvu"] = new SelectList(_context.Dichvus, "Madichvu", "Madichvu", khachhang.Madichvu);
            ViewData["Maduan"] = new SelectList(_context.Duans, "Maduan", "Maduan", khachhang.Maduan);
            return View(khachhang);
        }

        // POST: Admin/Khachhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Makh,Tenkh,Diachi,Dt,Email,Madichvu,Maduan,Ghichu")] Khachhang khachhang)
        {
            if (id != khachhang.Makh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.Makh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Madichvu"] = new SelectList(_context.Dichvus, "Madichvu", "Madichvu", khachhang.Madichvu);
            ViewData["Maduan"] = new SelectList(_context.Duans, "Maduan", "Maduan", khachhang.Maduan);
            return View(khachhang);
        }

        // GET: Admin/Khachhangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .Include(k => k.MadichvuNavigation)
                .Include(k => k.MaduanNavigation)
                .FirstOrDefaultAsync(m => m.Makh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // POST: Admin/Khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var khachhang = await _context.Khachhangs.FindAsync(id);
            _context.Khachhangs.Remove(khachhang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachhangExists(string id)
        {
            return _context.Khachhangs.Any(e => e.Makh == id);
        }
    }
}

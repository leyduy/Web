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
    public class DuansController : Controller
    {
        private readonly DACNContext _context;
        private readonly INotyfService _notyfService;
        private INotyfService notyfService;
        public DuansController(DACNContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Duans
        [Route("Duan.html", Name = "DuanIndex")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Duans.ToListAsync());
        }

        // GET: Admin/Duans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duan = await _context.Duans
                .FirstOrDefaultAsync(m => m.Maduan == id);
            if (duan == null)
            {
                return NotFound();
            }

            return View(duan);
        }

        // GET: Admin/Duans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Duans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maduan,Loaiduan")] Duan duan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duan);
        }

        // GET: Admin/Duans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duan = await _context.Duans.FindAsync(id);
            if (duan == null)
            {
                return NotFound();
            }
            return View(duan);
        }

        // POST: Admin/Duans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Maduan,Loaiduan")] Duan duan)
        {
            if (id != duan.Maduan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuanExists(duan.Maduan))
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
            return View(duan);
        }

        // GET: Admin/Duans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duan = await _context.Duans
                .FirstOrDefaultAsync(m => m.Maduan == id);
            if (duan == null)
            {
                return NotFound();
            }

            return View(duan);
        }

        // POST: Admin/Duans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duan = await _context.Duans.FindAsync(id);
            _context.Duans.Remove(duan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuanExists(int id)
        {
            return _context.Duans.Any(e => e.Maduan == id);
        }
    }
}

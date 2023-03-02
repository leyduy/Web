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
    public class DichvusController : Controller
    {
        private readonly DACNContext _context;
        private readonly INotyfService _notyfService;
        private INotyfService notyfService;

        public DichvusController(DACNContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Dichvus
        [Route("Dichvu.html", Name = "DichvuIndex")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dichvus.ToListAsync());
        }

        // GET: Admin/Dichvus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichvu = await _context.Dichvus
                .FirstOrDefaultAsync(m => m.Madichvu == id);
            if (dichvu == null)
            {
                return NotFound();
            }

            return View(dichvu);
        }

        // GET: Admin/Dichvus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dichvus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Madichvu,Loaidichvu")] Dichvu dichvu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dichvu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dichvu);
        }

        // GET: Admin/Dichvus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichvu = await _context.Dichvus.FindAsync(id);
            if (dichvu == null)
            {
                return NotFound();
            }
            return View(dichvu);
        }

        // POST: Admin/Dichvus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Madichvu,Loaidichvu")] Dichvu dichvu)
        {
            if (id != dichvu.Madichvu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dichvu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichvuExists(dichvu.Madichvu))
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
            return View(dichvu);
        }

        // GET: Admin/Dichvus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichvu = await _context.Dichvus
                .FirstOrDefaultAsync(m => m.Madichvu == id);
            if (dichvu == null)
            {
                return NotFound();
            }

            return View(dichvu);
        }

        // POST: Admin/Dichvus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dichvu = await _context.Dichvus.FindAsync(id);
            _context.Dichvus.Remove(dichvu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DichvuExists(string id)
        {
            return _context.Dichvus.Any(e => e.Madichvu == id);
        }
    }
}

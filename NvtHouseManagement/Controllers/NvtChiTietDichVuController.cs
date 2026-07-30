using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NvtHouseManagement.Models;

namespace NvtHouseManagement.Controllers
{
    public class NvtChiTietDichVuController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtChiTietDichVuController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtChiTietDichVu
        public async Task<IActionResult> Index()
        {
            var nvtDbContext = _context.NvtChiTietDichVus.Include(n => n.DichVu).Include(n => n.HopDong);
            return View(await nvtDbContext.ToListAsync());
        }

        // GET: NvtChiTietDichVu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtChiTietDichVu = await _context.NvtChiTietDichVus
                .Include(n => n.DichVu)
                .Include(n => n.HopDong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtChiTietDichVu == null)
            {
                return NotFound();
            }

            return View(nvtChiTietDichVu);
        }

        // GET: NvtChiTietDichVu/Create
        public IActionResult Create()
        {
            ViewData["DichVuId"] = new SelectList(_context.NvtDichVus, "Id", "Id");
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id");
            return View();
        }

        // POST: NvtChiTietDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HopDongId,DichVuId,SoLuong")] NvtChiTietDichVu nvtChiTietDichVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nvtChiTietDichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DichVuId"] = new SelectList(_context.NvtDichVus, "Id", "Id", nvtChiTietDichVu.DichVuId);
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtChiTietDichVu.HopDongId);
            return View(nvtChiTietDichVu);
        }

        // GET: NvtChiTietDichVu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtChiTietDichVu = await _context.NvtChiTietDichVus.FindAsync(id);
            if (nvtChiTietDichVu == null)
            {
                return NotFound();
            }
            ViewData["DichVuId"] = new SelectList(_context.NvtDichVus, "Id", "Id", nvtChiTietDichVu.DichVuId);
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtChiTietDichVu.HopDongId);
            return View(nvtChiTietDichVu);
        }

        // POST: NvtChiTietDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HopDongId,DichVuId,SoLuong")] NvtChiTietDichVu nvtChiTietDichVu)
        {
            if (id != nvtChiTietDichVu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtChiTietDichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtChiTietDichVuExists(nvtChiTietDichVu.Id))
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
            ViewData["DichVuId"] = new SelectList(_context.NvtDichVus, "Id", "Id", nvtChiTietDichVu.DichVuId);
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtChiTietDichVu.HopDongId);
            return View(nvtChiTietDichVu);
        }

        // GET: NvtChiTietDichVu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtChiTietDichVu = await _context.NvtChiTietDichVus
                .Include(n => n.DichVu)
                .Include(n => n.HopDong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtChiTietDichVu == null)
            {
                return NotFound();
            }

            return View(nvtChiTietDichVu);
        }

        // POST: NvtChiTietDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtChiTietDichVu = await _context.NvtChiTietDichVus.FindAsync(id);
            if (nvtChiTietDichVu != null)
            {
                _context.NvtChiTietDichVus.Remove(nvtChiTietDichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtChiTietDichVuExists(int id)
        {
            return _context.NvtChiTietDichVus.Any(e => e.Id == id);
        }
    }
}

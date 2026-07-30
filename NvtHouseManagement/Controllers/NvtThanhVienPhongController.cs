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
    public class NvtThanhVienPhongController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtThanhVienPhongController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtThanhVienPhong
        public async Task<IActionResult> Index()
        {
            var nvtDbContext = _context.NvtThanhVienPhongs.Include(n => n.HopDong);
            return View(await nvtDbContext.ToListAsync());
        }

        // GET: NvtThanhVienPhong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtThanhVienPhong = await _context.NvtThanhVienPhongs
                .Include(n => n.HopDong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtThanhVienPhong == null)
            {
                return NotFound();
            }

            return View(nvtThanhVienPhong);
        }

        // GET: NvtThanhVienPhong/Create
        public IActionResult Create()
        {
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id");
            return View();
        }

        // POST: NvtThanhVienPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HopDongId,HoTen,Cccd,SoDienThoai,QuanHeVoiChuHo")] NvtThanhVienPhong nvtThanhVienPhong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nvtThanhVienPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtThanhVienPhong.HopDongId);
            return View(nvtThanhVienPhong);
        }

        // GET: NvtThanhVienPhong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtThanhVienPhong = await _context.NvtThanhVienPhongs.FindAsync(id);
            if (nvtThanhVienPhong == null)
            {
                return NotFound();
            }
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtThanhVienPhong.HopDongId);
            return View(nvtThanhVienPhong);
        }

        // POST: NvtThanhVienPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HopDongId,HoTen,Cccd,SoDienThoai,QuanHeVoiChuHo")] NvtThanhVienPhong nvtThanhVienPhong)
        {
            if (id != nvtThanhVienPhong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtThanhVienPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtThanhVienPhongExists(nvtThanhVienPhong.Id))
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
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtThanhVienPhong.HopDongId);
            return View(nvtThanhVienPhong);
        }

        // GET: NvtThanhVienPhong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtThanhVienPhong = await _context.NvtThanhVienPhongs
                .Include(n => n.HopDong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtThanhVienPhong == null)
            {
                return NotFound();
            }

            return View(nvtThanhVienPhong);
        }

        // POST: NvtThanhVienPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtThanhVienPhong = await _context.NvtThanhVienPhongs.FindAsync(id);
            if (nvtThanhVienPhong != null)
            {
                _context.NvtThanhVienPhongs.Remove(nvtThanhVienPhong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtThanhVienPhongExists(int id)
        {
            return _context.NvtThanhVienPhongs.Any(e => e.Id == id);
        }
    }
}

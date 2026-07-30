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
    public class NvtBaoCaoSuCoController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtBaoCaoSuCoController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtBaoCaoSuCo
        public async Task<IActionResult> Index()
        {
            var nvtDbContext = _context.NvtBaoCaoSuCos.Include(n => n.KhachThue).Include(n => n.Phong);
            return View(await nvtDbContext.OrderByDescending(n => n.NgayTao).ToListAsync());
        }

        // POST: NvtBaoCaoSuCo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhongId,TieuDe,NoiDung")] NvtBaoCaoSuCo nvtBaoCaoSuCo)
        {
            // Find active tenant for this room
            var activeHopDong = await _context.NvtHopDongs.FirstOrDefaultAsync(h => h.PhongId == nvtBaoCaoSuCo.PhongId && h.TrangThai == "Hiệu lực");
            if (activeHopDong != null)
            {
                nvtBaoCaoSuCo.KhachThueId = activeHopDong.KhachThueId;
            }
            else
            {
                // Fallback
                var firstTenant = await _context.NvtKhachThues.FirstOrDefaultAsync();
                if(firstTenant != null) nvtBaoCaoSuCo.KhachThueId = firstTenant.Id;
            }

            nvtBaoCaoSuCo.TrangThai = "Chờ xử lý";
            nvtBaoCaoSuCo.NgayTao = DateTime.Now;

            ModelState.Remove("TrangThai");
            ModelState.Remove("KhachThue");
            ModelState.Remove("Phong");
            
            if (ModelState.IsValid)
            {
                _context.Add(nvtBaoCaoSuCo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: NvtBaoCaoSuCo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var nvtBaoCaoSuCo = await _context.NvtBaoCaoSuCos.FindAsync(id);
            if (nvtBaoCaoSuCo == null) return NotFound();
            
            var phongs = _context.NvtPhongs.ToList();
            ViewData["PhongId"] = new SelectList(phongs, "Id", "SoPhong", nvtBaoCaoSuCo.PhongId);
            return View(nvtBaoCaoSuCo);
        }

        // POST: NvtBaoCaoSuCo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhongId,TieuDe,NoiDung,TrangThai")] NvtBaoCaoSuCo nvtBaoCaoSuCo)
        {
            if (id != nvtBaoCaoSuCo.Id) return NotFound();

            ModelState.Remove("Phong");
            ModelState.Remove("KhachThue");
            ModelState.Remove("KhachThueId");
            ModelState.Remove("NgayTao");
            
            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.NvtBaoCaoSuCos.FindAsync(id);
                    if (existing == null) return NotFound();

                    existing.PhongId = nvtBaoCaoSuCo.PhongId;
                    existing.TieuDe = nvtBaoCaoSuCo.TieuDe;
                    existing.NoiDung = nvtBaoCaoSuCo.NoiDung;
                    existing.TrangThai = nvtBaoCaoSuCo.TrangThai;

                    var activeHopDong = await _context.NvtHopDongs.FirstOrDefaultAsync(h => h.PhongId == nvtBaoCaoSuCo.PhongId && h.TrangThai == "Hiệu lực");
                    if (activeHopDong != null)
                    {
                        existing.KhachThueId = activeHopDong.KhachThueId;
                    }
                    
                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtBaoCaoSuCoExists(nvtBaoCaoSuCo.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            var phongs = _context.NvtPhongs.ToList();
            ViewData["PhongId"] = new SelectList(phongs, "Id", "SoPhong", nvtBaoCaoSuCo.PhongId);
            return View(nvtBaoCaoSuCo);
        }

        private bool NvtBaoCaoSuCoExists(int id)
        {
            return _context.NvtBaoCaoSuCos.Any(e => e.Id == id);
        }

        // POST: NvtBaoCaoSuCo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtBaoCaoSuCo = await _context.NvtBaoCaoSuCos.FindAsync(id);
            if (nvtBaoCaoSuCo != null)
            {
                _context.NvtBaoCaoSuCos.Remove(nvtBaoCaoSuCo);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: NvtBaoCaoSuCo/CapNhatTrangThai/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThaiMoi)
        {
            var suCo = await _context.NvtBaoCaoSuCos.FindAsync(id);
            if (suCo != null && !string.IsNullOrEmpty(trangThaiMoi))
            {
                suCo.TrangThai = trangThaiMoi;
                _context.Update(suCo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

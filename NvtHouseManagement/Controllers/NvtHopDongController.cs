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
    public class NvtHopDongController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtHopDongController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtHopDong
        public async Task<IActionResult> Index()
        {
            var nvtDbContext = _context.NvtHopDongs.Include(n => n.KhachThue).Include(n => n.Phong);
            return View(await nvtDbContext.ToListAsync());
        }

        // GET: NvtHopDong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtHopDong = await _context.NvtHopDongs
                .Include(n => n.KhachThue)
                .Include(n => n.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (nvtHopDong == null)
            {
                return NotFound();
            }

            return View(nvtHopDong);
        }

        // GET: NvtHopDong/Create
        public IActionResult Create()
        {
            ViewData["KhachThueId"] = new SelectList(_context.NvtKhachThues, "Id", "HoTen");

            // Lấy toàn bộ danh sách phòng để hiện lên Dropdown
            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "SoPhong");

            return View();
        }

        // POST: NvtHopDong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhongId,KhachThueId,NgayBatDau,NgayKetThuc,TienCoc,GiaThueThucTe,TrangThai")] NvtHopDong nvtHopDong)
        {
            // 1. TỰ ĐỘNG GÁN GIÁ TRỊ TRẠNG THÁI
            nvtHopDong.TrangThai = "Hiệu lực";

            // 2. XÓA BỎ BẮT LỖI REQUIRED CHO CÁC TRƯỜNG NÀY
            ModelState.Remove("TrangThai");
            ModelState.Remove("Phong");
            ModelState.Remove("KhachThue");
            ModelState.Remove("NvtChiTietDichVus");
            ModelState.Remove("NvtHoaDons");
            ModelState.Remove("NvtThanhVienPhongs");

            if (ModelState.IsValid)
            {
                _context.Add(nvtHopDong);
                await _context.SaveChangesAsync();

                // Tự động đổi trạng thái phòng thành "Đã thuê"
                var phong = await _context.NvtPhongs.FindAsync(nvtHopDong.PhongId);
                if (phong != null)
                {
                    phong.TrangThai = "Đã thuê";
                    _context.Update(phong);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["KhachThueId"] = new SelectList(_context.NvtKhachThues, "Id", "HoTen", nvtHopDong.KhachThueId);
            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "SoPhong", nvtHopDong.PhongId);
            return View(nvtHopDong);
        }

        // GET: NvtHopDong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtHopDong = await _context.NvtHopDongs.FindAsync(id);
            if (nvtHopDong == null)
            {
                return NotFound();
            }
            ViewData["KhachThueId"] = new SelectList(_context.NvtKhachThues, "Id", "HoTen", nvtHopDong.KhachThueId);
            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "SoPhong", nvtHopDong.PhongId);
            return View(nvtHopDong);
        }

        // POST: NvtHopDong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhongId,KhachThueId,NgayBatDau,NgayKetThuc,TienCoc,GiaThueThucTe,TrangThai")] NvtHopDong nvtHopDong)
        {
            if (id != nvtHopDong.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Phong");
            ModelState.Remove("KhachThue");
            ModelState.Remove("NvtChiTietDichVus");
            ModelState.Remove("NvtHoaDons");
            ModelState.Remove("NvtThanhVienPhongs");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtHopDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtHopDongExists(nvtHopDong.Id))
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
            ViewData["KhachThueId"] = new SelectList(_context.NvtKhachThues, "Id", "HoTen", nvtHopDong.KhachThueId);
            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "SoPhong", nvtHopDong.PhongId);
            return View(nvtHopDong);
        }

        // GET: NvtHopDong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtHopDong = await _context.NvtHopDongs
                .Include(n => n.KhachThue)
                .Include(n => n.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (nvtHopDong == null)
            {
                return NotFound();
            }

            return View(nvtHopDong);
        }

        // POST: NvtHopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtHopDong = await _context.NvtHopDongs.FindAsync(id);
            if (nvtHopDong != null)
            {
                int phongId = nvtHopDong.PhongId;

                // Xóa hợp đồng
                _context.NvtHopDongs.Remove(nvtHopDong);
                await _context.SaveChangesAsync();

                // === LOGIC MỚI: Trả lại trạng thái "Trống" cho phòng khi hủy hợp đồng ===
                var phong = await _context.NvtPhongs.FindAsync(phongId);
                if (phong != null)
                {
                    phong.TrangThai = "Trống";
                    _context.Update(phong);
                    await _context.SaveChangesAsync();
                }
                // ======================================================================
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NvtHopDongExists(int id)
        {
            return _context.NvtHopDongs.Any(e => e.Id == id);
        }
    }
}
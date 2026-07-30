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
    public class NvtChotDienNuocController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtChotDienNuocController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtChotDienNuoc
        public async Task<IActionResult> Index()
        {
            var nvtDbContext = _context.NvtChotDienNuocs.Include(n => n.Phong);
            return View(await nvtDbContext.ToListAsync());
        }

        // GET: NvtChotDienNuoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtChotDienNuoc = await _context.NvtChotDienNuocs
                .Include(n => n.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtChotDienNuoc == null)
            {
                return NotFound();
            }

            return View(nvtChotDienNuoc);
        }

        // GET: NvtChotDienNuoc/Create
        public IActionResult Create()
        {
            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "SoPhong");
            return View();
        }

        // POST: NvtChotDienNuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: NvtChotDienNuoc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhongId,Thang,Nam,SoDienCu,SoDienMoi,SoNuocCu,SoNuocMoi,NgayChot")] NvtChotDienNuoc nvtChotDienNuoc)
        {
            // 1. Bỏ qua validation các thuộc tính liên kết
            ModelState.Remove("Phong");

            if (nvtChotDienNuoc.NgayChot == default)
            {
                nvtChotDienNuoc.NgayChot = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                // 2. Lưu bản ghi Chốt điện nước
                _context.Add(nvtChotDienNuoc);
                await _context.SaveChangesAsync();

                // 3. Tìm Hợp đồng đang có hiệu lực của phòng này
                var hopDong = await _context.NvtHopDongs
                    .FirstOrDefaultAsync(h => h.PhongId == nvtChotDienNuoc.PhongId && (h.TrangThai == "Hiệu lực" || h.TrangThai == null));

                if (hopDong != null)
                {
                    // 4. Tính tiền điện & nước (Đơn giá: Điện 3.500đ/kWh, Nước 10.000đ/m3)
                    decimal tienDien = (nvtChotDienNuoc.SoDienMoi - nvtChotDienNuoc.SoDienCu) * 3500m;
                    decimal tienNuoc = (nvtChotDienNuoc.SoNuocMoi - nvtChotDienNuoc.SoNuocCu) * 10000m;
                    decimal tienPhong = hopDong.GiaThueThucTe;
                    decimal tienDichVuKhac = 50000m; // Phí rác/wifi cố định

                    // 5. Tự động khởi tạo Hóa Đơn
                    var hoaDon = new NvtHoaDon
                    {
                        HopDongId = hopDong.Id,
                        Thang = nvtChotDienNuoc.Thang,
                        Nam = nvtChotDienNuoc.Nam,
                        TienPhong = tienPhong,
                        TienDien = tienDien,
                        TienNuoc = tienNuoc,
                        TienDichVuKhac = tienDichVuKhac,
                        TongTien = tienPhong + tienDien + tienNuoc + tienDichVuKhac,
                        TrangThai = "Chưa thanh toán",
                        NgayTao = DateTime.Now
                    };

                    _context.NvtHoaDons.Add(hoaDon);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "SoPhong", nvtChotDienNuoc.PhongId);
            return View(nvtChotDienNuoc);
        }

        // GET: NvtChotDienNuoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtChotDienNuoc = await _context.NvtChotDienNuocs.FindAsync(id);
            if (nvtChotDienNuoc == null)
            {
                return NotFound();
            }
            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "Id", nvtChotDienNuoc.PhongId);
            return View(nvtChotDienNuoc);
        }

        // POST: NvtChotDienNuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhongId,Thang,Nam,SoDienCu,SoDienMoi,SoNuocCu,SoNuocMoi,NgayChot")] NvtChotDienNuoc nvtChotDienNuoc)
        {
            if (id != nvtChotDienNuoc.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Phong");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtChotDienNuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtChotDienNuocExists(nvtChotDienNuoc.Id))
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
            ViewData["PhongId"] = new SelectList(_context.NvtPhongs, "Id", "Id", nvtChotDienNuoc.PhongId);
            return View(nvtChotDienNuoc);
        }

        // GET: NvtChotDienNuoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtChotDienNuoc = await _context.NvtChotDienNuocs
                .Include(n => n.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtChotDienNuoc == null)
            {
                return NotFound();
            }

            return View(nvtChotDienNuoc);
        }

        // POST: NvtChotDienNuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtChotDienNuoc = await _context.NvtChotDienNuocs.FindAsync(id);
            if (nvtChotDienNuoc != null)
            {
                _context.NvtChotDienNuocs.Remove(nvtChotDienNuoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtChotDienNuocExists(int id)
        {
            return _context.NvtChotDienNuocs.Any(e => e.Id == id);
        }
    }
}

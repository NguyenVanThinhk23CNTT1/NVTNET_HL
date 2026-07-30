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
    public class NvtHoaDonController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtHoaDonController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtHoaDon
        public async Task<IActionResult> Index()
        {
            var nvtDbContext = _context.NvtHoaDons
                .Include(n => n.HopDong).ThenInclude(h => h.Phong)
                .Include(n => n.HopDong).ThenInclude(h => h.KhachThue);
            return View(await nvtDbContext.ToListAsync());
        }

        // GET: NvtHoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtHoaDon = await _context.NvtHoaDons
                .Include(n => n.HopDong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtHoaDon == null)
            {
                return NotFound();
            }

            return View(nvtHoaDon);
        }

        // GET: NvtHoaDon/Create
        public IActionResult Create()
        {
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id");
            return View();
        }

        // POST: NvtHoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HopDongId,Thang,Nam,TienPhong,TienDien,TienNuoc,TienDichVuKhac,TongTien,TrangThai,NgayTao,NgayThanhToan")] NvtHoaDon nvtHoaDon)
        {
            ModelState.Remove("HopDong");
            if (ModelState.IsValid)
            {
                _context.Add(nvtHoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtHoaDon.HopDongId);
            return View(nvtHoaDon);
        }

        // GET: NvtHoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtHoaDon = await _context.NvtHoaDons.FindAsync(id);
            if (nvtHoaDon == null)
            {
                return NotFound();
            }
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtHoaDon.HopDongId);
            return View(nvtHoaDon);
        }

        // POST: NvtHoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HopDongId,Thang,Nam,TienPhong,TienDien,TienNuoc,TienDichVuKhac,TongTien,TrangThai,NgayTao,NgayThanhToan")] NvtHoaDon nvtHoaDon)
        {
            if (id != nvtHoaDon.Id)
            {
                return NotFound();
            }

            ModelState.Remove("HopDong");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtHoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtHoaDonExists(nvtHoaDon.Id))
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
            ViewData["HopDongId"] = new SelectList(_context.NvtHopDongs, "Id", "Id", nvtHoaDon.HopDongId);
            return View(nvtHoaDon);
        }

        // GET: NvtHoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtHoaDon = await _context.NvtHoaDons
                .Include(n => n.HopDong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtHoaDon == null)
            {
                return NotFound();
            }

            return View(nvtHoaDon);
        }

        // POST: NvtHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtHoaDon = await _context.NvtHoaDons.FindAsync(id);
            if (nvtHoaDon != null)
            {
                _context.NvtHoaDons.Remove(nvtHoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtHoaDonExists(int id)
        {
            return _context.NvtHoaDons.Any(e => e.Id == id);
        }

        // POST: NvtHoaDon/ThanhToan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThanhToan(int id)
        {
            var hoaDon = await _context.NvtHoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                hoaDon.TrangThai = "Đã thanh toán";
                hoaDon.NgayThanhToan = DateTime.Now;
                _context.Update(hoaDon);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: NvtHoaDon/CapNhatTrangThai/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThaiMoi)
        {
            var hoaDon = await _context.NvtHoaDons.FindAsync(id);
            if (hoaDon != null && !string.IsNullOrEmpty(trangThaiMoi))
            {
                hoaDon.TrangThai = trangThaiMoi;
                if (trangThaiMoi == "Đã thanh toán" && hoaDon.NgayThanhToan == null)
                {
                    hoaDon.NgayThanhToan = DateTime.Now;
                }
                else if (trangThaiMoi != "Đã thanh toán")
                {
                    hoaDon.NgayThanhToan = null;
                }
                _context.Update(hoaDon);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

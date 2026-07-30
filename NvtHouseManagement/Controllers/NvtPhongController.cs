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
    public class NvtPhongController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtPhongController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtPhong
        public async Task<IActionResult> Index(int? dayTroId, int? loaiPhongId)
        {
            IQueryable<NvtPhong> query = _context.NvtPhongs.Include(n => n.DayTro).Include(n => n.LoaiPhong);

            if (dayTroId.HasValue && dayTroId.Value > 0)
            {
                query = query.Where(p => p.DayTroId == dayTroId.Value);
            }

            if (loaiPhongId.HasValue && loaiPhongId.Value > 0)
            {
                query = query.Where(p => p.LoaiPhongId == loaiPhongId.Value);
            }

            ViewData["DayTroId"] = new SelectList(await _context.NvtDayTros.ToListAsync(), "Id", "TenDayTro", dayTroId);
            ViewData["LoaiPhongId"] = new SelectList(await _context.NvtLoaiPhongs.ToListAsync(), "Id", "TenLoaiPhong", loaiPhongId);

            return View(await query.ToListAsync());
        }

        // GET: NvtPhong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtPhong = await _context.NvtPhongs
                .Include(n => n.DayTro)
                .Include(n => n.LoaiPhong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtPhong == null)
            {
                return NotFound();
            }

            return View(nvtPhong);
        }

        // GET: NvtPhong/Create
        // GET: NvtPhong/Create
        public IActionResult Create()
        {
            // Đổi "Id" thành "TenDayTro" và "TenLoaiPhong"
            ViewData["DayTroId"] = new SelectList(_context.NvtDayTros, "Id", "TenDayTro");
            ViewData["LoaiPhongId"] = new SelectList(_context.NvtLoaiPhongs, "Id", "TenLoaiPhong");
            return View();
        }

        // POST: NvtPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: NvtPhong/Create
        // POST: NvtPhong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SoPhong,Tang,TrangThai,DayTroId,LoaiPhongId")] NvtPhong nvtPhong)
        {
            // 1. Loại bỏ các validation gây nghẽn
            ModelState.Remove("DayTro");
            ModelState.Remove("LoaiPhong");
            ModelState.Remove("TrangThai");

            // 2. Mặc định gán Tầng = 1 nếu không nhập
            if (nvtPhong.Tang <= 0)
            {
                nvtPhong.Tang = 1;
            }

            // 3. Mặc định gán Trạng thái = "Trống"
            if (string.IsNullOrEmpty(nvtPhong.TrangThai))
            {
                nvtPhong.TrangThai = "Trống";
            }

            if (ModelState.IsValid)
            {
                _context.Add(nvtPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu vẫn dính lỗi khác thì load lại dropdown
            ViewData["DayTroId"] = new SelectList(_context.NvtDayTros, "Id", "TenDayTro", nvtPhong.DayTroId);
            ViewData["LoaiPhongId"] = new SelectList(_context.NvtLoaiPhongs, "Id", "TenLoaiPhong", nvtPhong.LoaiPhongId);
            return View(nvtPhong);
        }

        // GET: NvtPhong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtPhong = await _context.NvtPhongs.FindAsync(id);
            if (nvtPhong == null)
            {
                return NotFound();
            }
            ViewData["DayTroId"] = new SelectList(_context.NvtDayTros, "Id", "TenDayTro", nvtPhong.DayTroId);
            ViewData["LoaiPhongId"] = new SelectList(_context.NvtLoaiPhongs, "Id", "TenLoaiPhong", nvtPhong.LoaiPhongId);
            return View(nvtPhong);
        }

        // POST: NvtPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SoPhong,Tang,TrangThai,DayTroId,LoaiPhongId")] NvtPhong nvtPhong)
        {
            if (id != nvtPhong.Id)
            {
                return NotFound();
            }
            
            ModelState.Remove("DayTro");
            ModelState.Remove("LoaiPhong");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtPhongExists(nvtPhong.Id))
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
            ViewData["DayTroId"] = new SelectList(_context.NvtDayTros, "Id", "TenDayTro", nvtPhong.DayTroId);
            ViewData["LoaiPhongId"] = new SelectList(_context.NvtLoaiPhongs, "Id", "TenLoaiPhong", nvtPhong.LoaiPhongId);
            return View(nvtPhong);
        }

        // GET: NvtPhong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtPhong = await _context.NvtPhongs
                .Include(n => n.DayTro)
                .Include(n => n.LoaiPhong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtPhong == null)
            {
                return NotFound();
            }

            return View(nvtPhong);
        }

        // POST: NvtPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtPhong = await _context.NvtPhongs.FindAsync(id);
            if (nvtPhong != null)
            {
                _context.NvtPhongs.Remove(nvtPhong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtPhongExists(int id)
        {
            return _context.NvtPhongs.Any(e => e.Id == id);
        }
    }
}

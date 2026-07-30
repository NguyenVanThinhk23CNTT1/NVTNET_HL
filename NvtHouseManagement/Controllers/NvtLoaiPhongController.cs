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
    public class NvtLoaiPhongController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtLoaiPhongController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtLoaiPhong
        public async Task<IActionResult> Index()
        {
            return View(await _context.NvtLoaiPhongs.Include(l => l.NvtPhongs).ToListAsync());
        }

        // GET: NvtLoaiPhong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtLoaiPhong = await _context.NvtLoaiPhongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtLoaiPhong == null)
            {
                return NotFound();
            }

            return View(nvtLoaiPhong);
        }

        // GET: NvtLoaiPhong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NvtLoaiPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenLoaiPhong,GiaCoBan,SoNguoiToiDa,MoTa")] NvtLoaiPhong nvtLoaiPhong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nvtLoaiPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nvtLoaiPhong);
        }

        // GET: NvtLoaiPhong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtLoaiPhong = await _context.NvtLoaiPhongs.FindAsync(id);
            if (nvtLoaiPhong == null)
            {
                return NotFound();
            }
            return View(nvtLoaiPhong);
        }

        // POST: NvtLoaiPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenLoaiPhong,GiaCoBan,SoNguoiToiDa,MoTa")] NvtLoaiPhong nvtLoaiPhong)
        {
            if (id != nvtLoaiPhong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtLoaiPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtLoaiPhongExists(nvtLoaiPhong.Id))
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
            return View(nvtLoaiPhong);
        }

        // GET: NvtLoaiPhong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtLoaiPhong = await _context.NvtLoaiPhongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtLoaiPhong == null)
            {
                return NotFound();
            }

            return View(nvtLoaiPhong);
        }

        // POST: NvtLoaiPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtLoaiPhong = await _context.NvtLoaiPhongs.FindAsync(id);
            if (nvtLoaiPhong != null)
            {
                _context.NvtLoaiPhongs.Remove(nvtLoaiPhong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtLoaiPhongExists(int id)
        {
            return _context.NvtLoaiPhongs.Any(e => e.Id == id);
        }
    }
}

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
    public class NvtDichVuController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtDichVuController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtDichVu
        public async Task<IActionResult> Index()
        {
            return View(await _context.NvtDichVus.ToListAsync());
        }

        // GET: NvtDichVu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtDichVu = await _context.NvtDichVus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtDichVu == null)
            {
                return NotFound();
            }

            return View(nvtDichVu);
        }

        // GET: NvtDichVu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NvtDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenDichVu,DonGia,DonViTinh")] NvtDichVu nvtDichVu)
        {
            ModelState.Remove("NvtChiTietDichVus");

            if (ModelState.IsValid)
            {
                _context.Add(nvtDichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nvtDichVu);
        }

        // GET: NvtDichVu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtDichVu = await _context.NvtDichVus.FindAsync(id);
            if (nvtDichVu == null)
            {
                return NotFound();
            }
            return View(nvtDichVu);
        }

        // POST: NvtDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenDichVu,DonGia,DonViTinh")] NvtDichVu nvtDichVu)
        {
            if (id != nvtDichVu.Id)
            {
                return NotFound();
            }

            ModelState.Remove("NvtChiTietDichVus");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtDichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtDichVuExists(nvtDichVu.Id))
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
            return View(nvtDichVu);
        }

        // GET: NvtDichVu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtDichVu = await _context.NvtDichVus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtDichVu == null)
            {
                return NotFound();
            }

            return View(nvtDichVu);
        }

        // POST: NvtDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtDichVu = await _context.NvtDichVus.FindAsync(id);
            if (nvtDichVu != null)
            {
                _context.NvtDichVus.Remove(nvtDichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtDichVuExists(int id)
        {
            return _context.NvtDichVus.Any(e => e.Id == id);
        }
    }
}

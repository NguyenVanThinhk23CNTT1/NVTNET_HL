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
    public class NvtDayTroController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtDayTroController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtDayTro
        public async Task<IActionResult> Index()
        {
            return View(await _context.NvtDayTros.Include(d => d.NvtPhongs).ToListAsync());
        }

        // GET: NvtDayTro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtDayTro = await _context.NvtDayTros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtDayTro == null)
            {
                return NotFound();
            }

            return View(nvtDayTro);
        }

        // GET: NvtDayTro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NvtDayTro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenDayTro,DiaChi,MoTa")] NvtDayTro nvtDayTro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nvtDayTro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nvtDayTro);
        }

        // GET: NvtDayTro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtDayTro = await _context.NvtDayTros.FindAsync(id);
            if (nvtDayTro == null)
            {
                return NotFound();
            }
            return View(nvtDayTro);
        }

        // POST: NvtDayTro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenDayTro,DiaChi,MoTa")] NvtDayTro nvtDayTro)
        {
            if (id != nvtDayTro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtDayTro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtDayTroExists(nvtDayTro.Id))
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
            return View(nvtDayTro);
        }

        // GET: NvtDayTro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtDayTro = await _context.NvtDayTros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtDayTro == null)
            {
                return NotFound();
            }

            return View(nvtDayTro);
        }

        // POST: NvtDayTro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtDayTro = await _context.NvtDayTros.FindAsync(id);
            if (nvtDayTro != null)
            {
                _context.NvtDayTros.Remove(nvtDayTro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtDayTroExists(int id)
        {
            return _context.NvtDayTros.Any(e => e.Id == id);
        }
    }
}

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
    public class NvtKhachThueController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtKhachThueController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: NvtKhachThue
        public async Task<IActionResult> Index()
        {
            var khachThues = await _context.NvtKhachThues
                .Include(k => k.NvtHopDongs)
                .ToListAsync();
            return View(khachThues);
        }

        // GET: NvtKhachThue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtKhachThue = await _context.NvtKhachThues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtKhachThue == null)
            {
                return NotFound();
            }

            return View(nvtKhachThue);
        }

        // GET: NvtKhachThue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NvtKhachThue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoTen,SoDienThoai,Cccd,Email,QueQuan,UserId")] NvtKhachThue nvtKhachThue)
        {
            ModelState.Remove("NvtBaoCaoSuCos");
            ModelState.Remove("NvtHopDongs");
            
            if (ModelState.IsValid)
            {
                _context.Add(nvtKhachThue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nvtKhachThue);
        }

        // GET: NvtKhachThue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtKhachThue = await _context.NvtKhachThues.FindAsync(id);
            if (nvtKhachThue == null)
            {
                return NotFound();
            }
            return View(nvtKhachThue);
        }

        // POST: NvtKhachThue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoTen,SoDienThoai,Cccd,Email,QueQuan,UserId")] NvtKhachThue nvtKhachThue)
        {
            if (id != nvtKhachThue.Id)
            {
                return NotFound();
            }
            
            ModelState.Remove("NvtBaoCaoSuCos");
            ModelState.Remove("NvtHopDongs");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvtKhachThue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NvtKhachThueExists(nvtKhachThue.Id))
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
            return View(nvtKhachThue);
        }

        // GET: NvtKhachThue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvtKhachThue = await _context.NvtKhachThues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nvtKhachThue == null)
            {
                return NotFound();
            }

            return View(nvtKhachThue);
        }

        // POST: NvtKhachThue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nvtKhachThue = await _context.NvtKhachThues.FindAsync(id);
            if (nvtKhachThue != null)
            {
                _context.NvtKhachThues.Remove(nvtKhachThue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NvtKhachThueExists(int id)
        {
            return _context.NvtKhachThues.Any(e => e.Id == id);
        }
    }
}

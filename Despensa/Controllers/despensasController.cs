using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Despensa.Data;
using Despensa.Models;

namespace Despensa.Controllers
{
    public class despensasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public despensasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: despensas
        public async Task<IActionResult> Index()
        {
            return View(await _context.despensa.Include(x => x.Products).ToListAsync());
        }

        public async Task<IActionResult> Pantry()
        {
            return View(await _context.despensa.Where(x=>x.Name==User.Identity.Name).Include(x => x.Products).ToListAsync());
        }

        public async Task<IActionResult> Pantries()
        {
            return View(await _context.despensa.Include(x => x.Products).ToListAsync());
        }

        // GET: despensas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despensa = await _context.despensa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (despensa == null)
            {
                return NotFound();
            }

            return View(despensa);
        }

        // GET: despensas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: despensas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] despensa despensa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(despensa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(despensa);
        }

        // GET: despensas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despensa = await _context.despensa.FindAsync(id);
            if (despensa == null)
            {
                return NotFound();
            }
            return View(despensa);
        }

        // POST: despensas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] despensa despensa)
        {
            if (id != despensa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(despensa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!despensaExists(despensa.Id))
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
            return View(despensa);
        }

        // GET: despensas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despensa = await _context.despensa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (despensa == null)
            {
                return NotFound();
            }

            return View(despensa);
        }

        // POST: despensas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var despensa = await _context.despensa.FindAsync(id);
            _context.despensa.Remove(despensa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool despensaExists(int id)
        {
            return _context.despensa.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuelPredictor.Data;
using FuelPredictor.Models.V2;

namespace FuelPredictor.Controllers
{
    public class CarburantsController : Controller
    {
        private readonly FuelPredictorContext _context;

        public CarburantsController(FuelPredictorContext context)
        {
            _context = context;
        }

        // GET: Carburants
        public async Task<IActionResult> Index()
        {
              return _context.Carburant != null ? 
                          View(await _context.Carburant.ToListAsync()) :
                          Problem("Entity set 'FuelPredictorContext.Carburant'  is null.");
        }

        // GET: Carburants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carburant == null)
            {
                return NotFound();
            }

            var carburant = await _context.Carburant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carburant == null)
            {
                return NotFound();
            }

            return View(carburant);
        }

        // GET: Carburants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carburants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeCarburant,Id")] Carburant carburant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carburant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carburant);
        }

        // GET: Carburants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carburant == null)
            {
                return NotFound();
            }

            var carburant = await _context.Carburant.FindAsync(id);
            if (carburant == null)
            {
                return NotFound();
            }
            return View(carburant);
        }

        // POST: Carburants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeCarburant,Id")] Carburant carburant)
        {
            if (id != carburant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carburant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarburantExists(carburant.Id))
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
            return View(carburant);
        }

        // GET: Carburants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carburant == null)
            {
                return NotFound();
            }

            var carburant = await _context.Carburant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carburant == null)
            {
                return NotFound();
            }

            return View(carburant);
        }

        // POST: Carburants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carburant == null)
            {
                return Problem("Entity set 'FuelPredictorContext.Carburant'  is null.");
            }
            var carburant = await _context.Carburant.FindAsync(id);
            if (carburant != null)
            {
                _context.Carburant.Remove(carburant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarburantExists(int id)
        {
          return (_context.Carburant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

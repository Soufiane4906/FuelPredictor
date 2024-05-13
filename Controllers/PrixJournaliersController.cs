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
    public class PrixJournaliersController : Controller
    {
        private readonly FuelPredictorContext _context;

        public PrixJournaliersController(FuelPredictorContext context)
        {
            _context = context;
        }

        // GET: PrixJournaliers
        public async Task<IActionResult> Index()
        {
            var fuelPredictorContext = _context.PrixJournalier.Include(p => p.Carburant).Include(p => p.Station);
            return View(await fuelPredictorContext.ToListAsync());
        }

        public async Task<IActionResult> IndexStation(int? id)
        {
            // Récupérer la liste des stations
            var stations = await _context.Station.ToListAsync();

            // Sélectionner la station correspondant à l'ID passé en paramètre
            var selectedStation = stations.FirstOrDefault(s => s.Id == id);

            // Créer la liste déroulante des stations en sélectionnant la station correspondante
            var stationList = new SelectList(stations, "Id", "Nom", selectedStation?.Id);

            ViewData["StationList"] = stationList;
            ViewData["CarburantList"] = new SelectList(_context.Carburant, "Id", "TypeCarburant");
            if (id == null || _context.PrixJournalier == null)
            {
                return NotFound();
            }
            var fuelPredictorContext = _context.PrixJournalier.Include(p => p.Carburant).Include(p => p.Station).Where(u=>u.IDStation==id);
            return View(await fuelPredictorContext.ToListAsync());
        }

        // GET: PrixJournaliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PrixJournalier == null)
            {
                return NotFound();
            }

            var prixJournalier = await _context.PrixJournalier
                .Include(p => p.Carburant)
                .Include(p => p.Station)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prixJournalier == null)
            {
                return NotFound();
            }

            return View(prixJournalier);
        }

        public IActionResult Create()
        {
            ViewData["IDCarburant"] = new SelectList(_context.Carburant, "Id", "TypeCarburant");
            ViewData["IDStation"] = new SelectList(_context.Station, "Id", "Nom");
            return View();
        }
        public IActionResult CreateForStation(int id)
        {
            var stations =  _context.Station.ToList();

            // Sélectionner la station correspondant à l'ID passé en paramètre
            var selectedStation = stations.FirstOrDefault(s => s.Id == id);

            // Créer la liste déroulante des stations en sélectionnant la station correspondante
            var stationList = new SelectList(stations, "Id", "Nom", selectedStation?.Id);

            ViewData["StationList"] = stationList;
            ViewData["CarburantList"] = new SelectList(_context.Carburant, "Id", "TypeCarburant");
     
            return View();
        }
        // GET: PrixJournaliers/Create

        // POST: PrixJournaliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("prix,date,IDStation,IDCarburant,Id")] PrixJournalier prixJournalier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prixJournalier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDCarburant"] = new SelectList(_context.Carburant, "Id", "TypeCarburant", prixJournalier.IDCarburant);
            ViewData["IDStation"] = new SelectList(_context.Station, "Id", "Nom", prixJournalier.IDStation);
            return View(prixJournalier);
        }

        // GET: PrixJournaliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PrixJournalier == null)
            {
                return NotFound();
            }

            var prixJournalier = await _context.PrixJournalier.FindAsync(id);
            if (prixJournalier == null)
            {
                return NotFound();
            }
            ViewData["IDCarburant"] = new SelectList(_context.Carburant, "Id", "TypeCarburant", prixJournalier.IDCarburant);
            ViewData["IDStation"] = new SelectList(_context.Station, "Id", "Nom", prixJournalier.IDStation);
            return View(prixJournalier);
        }

        // POST: PrixJournaliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("prix,date,IDStation,IDCarburant,Id")] PrixJournalier prixJournalier)
        {
            if (id != prixJournalier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prixJournalier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrixJournalierExists(prixJournalier.Id))
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
            ViewData["IDCarburant"] = new SelectList(_context.Carburant, "Id", "TypeCarburant", prixJournalier.IDCarburant);
            ViewData["IDStation"] = new SelectList(_context.Station, "Id", "Id", prixJournalier.IDStation);
            return View(prixJournalier);
        }

        // GET: PrixJournaliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PrixJournalier == null)
            {
                return NotFound();
            }

            var prixJournalier = await _context.PrixJournalier
                .Include(p => p.Carburant)
                .Include(p => p.Station)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prixJournalier == null)
            {
                return NotFound();
            }

            return View(prixJournalier);
        }

        // POST: PrixJournaliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PrixJournalier == null)
            {
                return Problem("Entity set 'FuelPredictorContext.PrixJournalier'  is null.");
            }
            var prixJournalier = await _context.PrixJournalier.FindAsync(id);
            if (prixJournalier != null)
            {
                _context.PrixJournalier.Remove(prixJournalier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrixJournalierExists(int id)
        {
          return (_context.PrixJournalier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

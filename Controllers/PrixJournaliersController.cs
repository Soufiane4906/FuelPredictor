using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuelPredictor.Data;
using FuelPredictor.Models.V2;
using FuelPredictor.Service;
using Microsoft.AspNetCore.Identity;
using FuelPredictor.Models.Users;

namespace FuelPredictor.Controllers
{
    public class PrixJournaliersController : Controller
    {
        private readonly FuelPredictorContext _context;
        private readonly PredictionService _predictionService;
        private readonly UserManager<ApplicationUser> _userManager;


        public PrixJournaliersController(FuelPredictorContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _predictionService = new PredictionService(context);
        }

        public async Task<IActionResult> PredictPrix(int stationId, int carburantId, DateTime date)
        {
            try
            {
                float predictedPrix = await _predictionService.PredictPrixJournalierAsync(stationId, carburantId, date);
                return Ok(new { predictedPrix });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }




        // GET: PrixJournaliers
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

          
            // Extract station IDs

                var fuelPredictorContext = _context.PrixJournalier
                    .Include(p => p.Carburant)
                    .Include(p => p.Station)
                    .Where(p => p.Station.IDGerant==currentUser.Id); // Filter by station IDs

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

        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            // Filter stations where IDGerant equals current user's ID
            var userStations = _context.Station.Where(s => s.IDGerant == currentUser.Id).ToList();

            ViewData["IDCarburant"] = new SelectList(_context.Carburant, "Id", "TypeCarburant");
            ViewData["IDStation"] = new SelectList(userStations, "Id", "Nom");

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
        [HttpGet]
        public async Task<IActionResult> PrixJournaliersByStation(int? stationId)
        {
            if (stationId == null)
            {
                return BadRequest("Station ID is required.");
            }

            var prixJournaliers = await _context.PrixJournalier
                .Where(p => p.IDStation == stationId)
                .Include(p => p.Carburant)
                .OrderBy(p => p.date)
                .GroupBy(p => p.IDCarburant)
                .Select(g => new
                {
                    idCarburant = g.Key,
                    carburant = g.FirstOrDefault().Carburant.TypeCarburant, // Assuming Carburant has a 'nom' property
                    prixData = g.Select(p => new
                    {
                        prix = p.prix,
                        date = p.date.ToString("dd-MM-yyyy")
                    }).ToList()
                })
                .ToListAsync();

            return Json(prixJournaliers);
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

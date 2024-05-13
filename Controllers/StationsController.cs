using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuelPredictor.Data;
using FuelPredictor.Models.V2;
using FuelPredictor.Models.Users;
using Microsoft.AspNetCore.Identity;
using static System.Collections.Specialized.BitVector32;
using FuelPredictor.Models.Dto;

namespace FuelPredictor.Controllers
{
    public class StationsController : Controller
    {
        private readonly FuelPredictorContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StationsController(FuelPredictorContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }




        // GET: Stations
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var fuelPredictorContext = _context.Station.Include(u=>u.Ville).Where(g=>g.IDGerant==currentUser.Id);
            return View(await fuelPredictorContext.ToListAsync());
        }


        [HttpPost]
        public IActionResult GetStations()
        {
            int pageSize = int.Parse(Request.Form["length"]);
            int skip = int.Parse(Request.Form["start"]);

            var searchValue = Request.Form["search[value]"];

            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            IQueryable<Station> stationsQuery = _context.Station.Include(s => s.Ville).Include(s => s.Company);

            if (!string.IsNullOrEmpty(searchValue))
            {
                stationsQuery = stationsQuery.Where(m =>
                    m.Adresse.Contains(searchValue) ||
                    m.Company.Nom.Contains(searchValue) ||
                    m.Ville.Name.Contains(searchValue)); // Corrected to use Ville instead of m.Ville.Name
            }

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                // Sorting logic, assuming sortColumn is the name of the column you want to sort by
                stationsQuery = sortColumnDirection == "asc"
                    ? stationsQuery.OrderBy(m => EF.Property<object>(m, sortColumn))
                    : stationsQuery.OrderByDescending(m => EF.Property<object>(m, sortColumn));
            }

            var stations = stationsQuery
        .Skip(skip)
        .Take(pageSize)
        .Select(s => new StationDto
        {
            Id=s.Id,
            Nom = s.Nom,
            Adresse = s.Adresse,
            Latitude = s.Latitude,
            Longitude = s.Longitude,
            IdVille = (int)s.IDVille,
            IdCompany = (int)s.IDCompany,
            Ville = s.Ville != null ? s.Ville.Name : null,
            Company = s.Company != null ? s.Company.Nom : null,
            PrixGasoilAujourdhui = s.PrixJournaliers
                .Where(p => p.Carburant.TypeCarburant == "Diesel" && p.date.Date == DateTime.Today)
                .Select(p => p.prix)
                .FirstOrDefault(),
            PrixEssenceAujourdhui = s.PrixJournaliers
                .Where(p => p.Carburant.TypeCarburant == "Essence" && p.date.Date == DateTime.Today)
                .Select(p => p.prix)
                .FirstOrDefault()
        })
        .ToList();


            var recordsTotal = _context.Station.Count();

            var jsonData = new { recordsFiltered = stations.Count(), recordsTotal, data = stations };

            return Ok(jsonData);
        }



        // GET: Stations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Station == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .Include(s => s.Gerant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // GET: Stations/Create
        public IActionResult Create()
        {
            ViewData["IDGerant"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["IDVille"] = new SelectList(_context.Ville, "Id", "Name");

            ViewData["IDCompany"] = new SelectList(_context.Companies, "Id", "Nom");
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Adresse,Latitude,Longitude,Id , IDVille  , IDCompany")] Station station)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    // Handle the case where currentUser is null
                    // You can return an error or redirect the user to a different page
                    return RedirectToAction("Error");
                }

                // Assigner l'ID de l'utilisateur courant à la station
                station.IDGerant = currentUser.Id;

                // Set the Gerant navigation property of the station
                station.Gerant = currentUser;
                _context.Add(station);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDVille"] = new SelectList(_context.Ville, "Id", "Name", station.IDVille);
            ViewData["IDCompany"] = new SelectList(_context.Companies, "Id", "Nom", station.IDCompany);
            return View(station);
        }

        // GET: Stations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["IDVille"] = new SelectList(_context.Ville, "Id", "Name");
            ViewData["IDCompany"] = new SelectList(_context.Companies, "Id", "Nom");

            if (id == null || _context.Station == null)
            {
                return NotFound();
            }

            var station = await _context.Station.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            //ViewData["IDGerant"] = new SelectList(_context.ApplicationUser, "Id", "Id", station.IDGerant);
            return View(station);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nom,Adresse,Latitude,Longitude,Id , IDVille,IDCompany")] Station station)
        {
            ViewData["IDVille"] = new SelectList(_context.Ville, "Id", "Name", station.IDVille);
            ViewData["IDCompany"] = new SelectList(_context.Companies, "Id", "Nom", station.IDCompany);

            if (id != station.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser currentUser = await _userManager.GetUserAsync(User);

                    // Set the IDGerant property of the station
                    station.IDGerant = currentUser.Id;

                    // Set the Gerant navigation property of the station
                    station.Gerant = currentUser;
                    _context.Update(station);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.Id))
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
          //  ViewData["IDGerant"] = new SelectList(_context.ApplicationUser, "Id", "Id", station.IDGerant);
            return View(station);
        }

        // GET: Stations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Station == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .Include(s => s.Gerant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Station == null)
            {
                return Problem("Entity set 'FuelPredictorContext.Station'  is null.");
            }
            var station = await _context.Station.FindAsync(id);
            if (station != null)
            {
                _context.Station.Remove(station);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(int id)
        {
          return (_context.Station?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

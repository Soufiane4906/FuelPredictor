using FuelPredictor.Data;
using FuelPredictor.Models;
using FuelPredictor.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;

namespace FuelPredictor.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly FuelPredictorContext _context;

        public HomeController(ILogger<HomeController> logger, FuelPredictorContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch all stations from the database
            // Inside your method
            var stations = await _context.Station.Select(s => new StationDto
            {
                Id= s.Id,
                Nom = s.Nom,
                Adresse = s.Adresse,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                IdVille = (int) s.IDVille,
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
.ToListAsync();

      


            return View(stations);
        }

        [HttpGet]
        public IActionResult GetPrixJournaliers(int stationId)
        {
            var prixJournaliers = _context.PrixJournalier
                .Where(p => p.IDStation == stationId).Include(u=>u.Carburant)
                .ToList();

            return Json(prixJournaliers);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
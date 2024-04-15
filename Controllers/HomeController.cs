using FuelPredictor.Data;
using FuelPredictor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FuelPredictor.Controllers
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
            var stations = await _context.Station.ToListAsync();

            // Create a list to store marker coordinates
/*            var markers = new List<Tuple<double, double>>();

            // Iterate through each station to extract coordinates
            foreach (var station in stations)
            {
                markers.Add(new Tuple<double, double>(station.Latitude, station.Longitude));
                string latitude, longitude;

                // Convert Latitude and Longitude strings to doubles
           
            }
            // Pass the markers to the view
            ViewBag.Markers = markers;*/

            return View(await _context.Station.ToListAsync());
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
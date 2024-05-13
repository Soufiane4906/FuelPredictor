using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuelPredictor.Data;
using FuelPredictor.Models.V2;
using Microsoft.AspNetCore.Hosting;

namespace FuelPredictor.Areas.SuperAdmin.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly FuelPredictorContext _context;
        private readonly IWebHostEnvironment _HostingEnvironment;

        public CompaniesController(FuelPredictorContext context, IWebHostEnvironment __HostingEnvironment)
        {
            _context = context;
            _HostingEnvironment = __HostingEnvironment;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return _context.Companies != null ?
                        View(await _context.Companies.ToListAsync()) :
                        Problem("Entity set 'FuelPredictorContext.Companies'  is null.");
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Nom,Pays,Adresse,Email,Telephone,photo")] Company company)
        {

            if (ModelState.IsValid)
            {
                if (company.photo != null && company.photo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_HostingEnvironment.WebRootPath, "images", "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + company.photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await company.photo.CopyToAsync(stream);
                    }

                    company.PhotoPath = "/uploads/" + uniqueFileName;
                }

                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Pays,Adresse,Email,Telephone,photo")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (company.photo != null && company.photo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_HostingEnvironment.WebRootPath, "images", "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + company.photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await company.photo.CopyToAsync(stream);
                    }

                    company.PhotoPath = "/uploads/" + uniqueFileName;
                }

                _context.Update(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }



        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'FuelPredictorContext.Companies'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

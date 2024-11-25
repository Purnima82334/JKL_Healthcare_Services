using JKLHealthcareSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JKLHealthcareSystem.Controllers
{
    [Authorize(Roles = "Admin,Caregiver")]
    public class CaregiverController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaregiverController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var caregivers = await _context.Caregivers
                .Include(c => c.Appointments)
                .ToListAsync();
            return View(caregivers);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Caregiver caregiver)
        {
            if (ModelState.IsValid)
            {
                _context.Caregivers.Add(caregiver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caregiver);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var caregiver = await _context.Caregivers.FindAsync(id);
            if (caregiver != null)
            {
                caregiver.Availability = !caregiver.Availability;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
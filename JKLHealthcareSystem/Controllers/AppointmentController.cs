using JKLHealthcareSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JKLHealthcareSystem.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Caregiver)
                .Include(a => a.Patient)
                .ToListAsync();

            if (User.IsInRole("Caregiver"))
            {
                var caregiverId = _context.Caregivers
                    .FirstOrDefault(c => c.Name == User.Identity.Name)?.CaregiverId;
                appointments = appointments.Where(a => a.CaregiverId == caregiverId).ToList();
            }
            else if (User.IsInRole("Patient"))
            {
                var patientId = _context.Patients
                    .FirstOrDefault(p => p.Name == User.Identity.Name)?.PatientId;
                appointments = appointments.Where(a => a.PatientId == patientId).ToList();
            }

            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Caregivers = await _context.Caregivers
                .Where(c => c.Availability)
                .ToListAsync();
            ViewBag.Patients = await _context.Patients.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Status = "Scheduled";
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Caregivers = await _context.Caregivers
                .Where(c => c.Availability)
                .ToListAsync();
            ViewBag.Patients = await _context.Patients.ToListAsync();
            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
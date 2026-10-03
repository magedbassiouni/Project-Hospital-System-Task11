using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Hospital_System_Task11.Data;
using Project_Hospital_System_Task11.Models;

namespace Project_Hospital_System_Task11.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HospitalDbContext _context;

        public AppointmentsController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Appointments/Book/5
        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(d => d.DoctorId == id);

            if (doctor == null)
                return NotFound();

            var model = new BookAppointmentViewModel
            {
                DoctorId = doctor.DoctorId,
                DoctorName = doctor.Name,
                SpecializationName = doctor.Specialization.Name,
                Date = DateTime.Today,
                Time = DateTime.Today.AddHours(9)
            };

            return View(model);
        }

        // POST: Appointments/Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(
            BookAppointmentViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PatientName))
            {
                ModelState.AddModelError(
                    "PatientName",
                    "Patient name is required.");
            }

            DateTime selectedDate = model.Date.Date;

            DateTime today = DateTime.Today;

            if (selectedDate < today)
            {
                ModelState.AddModelError(
                    "Date",
                    "You cannot book an appointment in the past.");
            }

            if (selectedDate.DayOfWeek == DayOfWeek.Friday ||
                selectedDate.DayOfWeek == DayOfWeek.Saturday)
            {
                ModelState.AddModelError(
                    "Date",
                    "Appointments are available Sunday to Thursday.");
            }

            TimeSpan selectedTime = model.Time.TimeOfDay;

            TimeSpan startTime =
                new TimeSpan(9, 0, 0);

            TimeSpan endTime =
                new TimeSpan(16, 30, 0);

            if (selectedTime < startTime ||
                selectedTime > endTime)
            {
                ModelState.AddModelError(
                    "Time",
                    "Time must be between 09:00 AM and 04:30 PM.");
            }

            if (selectedTime.Minutes != 0 &&
                selectedTime.Minutes != 30)
            {
                ModelState.AddModelError(
                    "Time",
                    "Appointments are every 30 minutes.");
            }

            var doctor = await _context.Doctors
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(
                    d => d.DoctorId == model.DoctorId);

            if (doctor == null)
                return NotFound();

            bool alreadyBooked =
                await _context.Appointments.AnyAsync(a =>
                    a.DoctorId == model.DoctorId &&
                    a.Date.Date == selectedDate &&
                    a.Time.TimeOfDay == selectedTime);

            if (alreadyBooked)
            {
                ModelState.AddModelError(
                    "",
                    "This doctor is already booked at this date and time.");
            }

            if (!ModelState.IsValid)
            {
                model.DoctorName = doctor.Name;
                model.SpecializationName =
                    doctor.Specialization.Name;

                return View(model);
            }

            var appointment = new Appointment
            {
                PatientName = model.PatientName,
                Date = selectedDate,
                Time = model.Time,
                DoctorId = model.DoctorId
            };

            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.Specialization)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .ToListAsync();

            return View(appointments);
        }
    }
}
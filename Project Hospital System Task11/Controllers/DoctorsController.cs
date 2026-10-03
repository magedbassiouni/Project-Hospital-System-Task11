using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Hospital_System_Task11.Data;
using Project_Hospital_System_Task11.Models;

namespace Project_Hospital_System_Task11.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly HospitalDbContext _context;

        public DoctorsController(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string search,
            int? specializationId,
            int page = 1)
        {
            int pageSize = 6;

            var query = _context.Doctors
                .Include(d => d.Specialization)
                .AsQueryable();

            // Search by doctor name
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d =>
                    d.Name.Contains(search));
            }

            // Filter by specialization
            if (specializationId.HasValue)
            {
                query = query.Where(d =>
                    d.SpecializationId == specializationId.Value);
            }

            int totalDoctors = await query.CountAsync();

            int totalPages =
                (int)Math.Ceiling(
                    totalDoctors / (double)pageSize);

            if (totalPages == 0)
                totalPages = 1;

            if (page < 1)
                page = 1;

            if (page > totalPages)
                page = totalPages;

            var doctors = await query
                .OrderBy(d => d.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var specializations =
                await _context.Specializations
                    .OrderBy(s => s.Name)
                    .ToListAsync();

            var model = new DoctorIndexViewModel
            {
                Doctors = doctors,
                Search = search,
                SpecializationId = specializationId,
                Specializations = specializations,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }
    }
}
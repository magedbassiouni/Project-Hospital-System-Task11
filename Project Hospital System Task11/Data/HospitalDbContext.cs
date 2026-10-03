using Microsoft.EntityFrameworkCore;
using Project_Hospital_System_Task11.Data.Configurations;
using Project_Hospital_System_Task11.Models;

namespace Project_Hospital_System_Task11.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(
            DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(
                new SpecializationConfiguration());

            modelBuilder.ApplyConfiguration(
                new DoctorConfiguration());

            modelBuilder.ApplyConfiguration(
                new AppointmentConfiguration());

            // Seed Specializations
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization
                {
                    SpecializationId = 1,
                    Name = "Cardiology"
                },
                new Specialization
                {
                    SpecializationId = 2,
                    Name = "Dentistry"
                },
                new Specialization
                {
                    SpecializationId = 3,
                    Name = "Pediatrics"
                },
                new Specialization
                {
                    SpecializationId = 4,
                    Name = "Dermatology"
                }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = 1,
                    Name = "Ahmed Mohamed",
                    Image = "/images/doctor1.jpg",
                    SpecializationId = 1
                },
                new Doctor
                {
                    DoctorId = 2,
                    Name = "Mohamed Ali",
                    Image = "/images/doctor2.jpg",
                    SpecializationId = 2
                },
                new Doctor
                {
                    DoctorId = 3,
                    Name = "Omar Hassan",
                    Image = "/images/doctor3.jpg",
                    SpecializationId = 3
                },
                new Doctor
                {
                    DoctorId = 4,
                    Name = "Youssef Ahmed",
                    Image = "/images/doctor4.jpg",
                    SpecializationId = 4
                }
            );
        }
    }
}
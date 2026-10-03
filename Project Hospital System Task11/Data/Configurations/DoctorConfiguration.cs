using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Hospital_System_Task11.Models;

namespace Project_Hospital_System_Task11.Data.Configurations
{
    public class DoctorConfiguration
        : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.DoctorId);

            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.Image)
                   .HasMaxLength(500);

            builder.HasOne(d => d.Specialization)
                   .WithMany(s => s.Doctors)
                   .HasForeignKey(d => d.SpecializationId);
        }
    }
}
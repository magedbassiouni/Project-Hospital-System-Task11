using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Hospital_System_Task11.Models;

namespace Project_Hospital_System_Task11.Data.Configurations
{
    public class SpecializationConfiguration
        : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(s => s.SpecializationId);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(s => s.Doctors)
                   .WithOne(d => d.Specialization)
                   .HasForeignKey(d => d.SpecializationId);
        }
    }
}
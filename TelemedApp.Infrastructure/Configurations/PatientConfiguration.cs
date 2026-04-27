using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Infrastructure.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .HasMaxLength(150);

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.Property(p => p.Gender)
                .HasMaxLength(10);

            builder.Property(p => p.Address)
                .HasMaxLength(300);

            builder.Property(p => p.InsuranceProvider)
                .HasMaxLength(150);

            builder.Property(p => p.InsuranceNo)
                .HasMaxLength(50);

            builder.Property(p => p.MedicalRecordNumber)
                .HasMaxLength(50);

            builder.Property(p => p.NextOfKin)
                .HasMaxLength(150);

            builder.Property(p => p.Notes)
                .HasMaxLength(2000);
        }
    }
}
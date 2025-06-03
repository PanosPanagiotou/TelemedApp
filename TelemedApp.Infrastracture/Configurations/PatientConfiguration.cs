using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelemedApp.Domain.Entities;

namespace TelemedApp.Infrastracture.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Email).HasMaxLength(150);
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.Gender).HasMaxLength(10);
        }
    }
}

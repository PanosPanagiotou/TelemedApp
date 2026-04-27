using Microsoft.EntityFrameworkCore;
using TelemedApp.Domain.Entities;
using TelemedApp.Infrastructure.Configurations;

namespace TelemedApp.Infrastructure.Data
{
    public class TelemedDbContext(DbContextOptions<TelemedDbContext> options) : DbContext(options)
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Doctor> Doctors => Set<Doctor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfiguration());

            // Future configurations can be added here
            base.OnModelCreating(modelBuilder);
        }
    }
}
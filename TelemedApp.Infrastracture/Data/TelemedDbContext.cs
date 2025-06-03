using Microsoft.EntityFrameworkCore;
using TelemedApp.Domain.Entities;
using TelemedApp.Infrastracture.Configurations;

namespace TelemedApp.Infrastracture.Data
{
    public class TelemedDbContext(DbContextOptions<TelemedDbContext> options) : DbContext(options)
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfiguration());

            // Future configurations can be added here
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastructure.Data;

namespace TelemedApp.Infrastructure.Repositories
{
    public class AppointmentRepository(TelemedDbContext context) : GenericRepository<Appointment>(context), IAppointmentRepository
    {
        public override async Task<Appointment?> GetAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingAsync(DateTime until)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.ScheduledAt >= DateTime.UtcNow &&
                            a.ScheduledAt <= until)
                .OrderBy(a => a.ScheduledAt)
                .ToListAsync();
        }


        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<bool> HasConflictAsync(Guid doctorId, DateTime start, DateTime end)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.ScheduledAt < end &&
                a.ScheduledAt.AddMinutes(30) > start
            );
        }

        public async Task<bool> PatientHasConflictAsync(Guid patientId, DateTime start, DateTime end)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.PatientId == patientId &&
                a.ScheduledAt < end &&
                a.ScheduledAt.AddMinutes(30) > start
            );
        }
    }
}
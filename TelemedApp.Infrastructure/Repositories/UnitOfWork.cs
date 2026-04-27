using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastructure.Data;

namespace TelemedApp.Infrastructure.Repositories
{
    public class UnitOfWork(
        TelemedDbContext context,
        IPatientRepository patients,
        IDoctorRepository doctors,
        IAppointmentRepository appointments,
        IMedicalRecordRepository medicalRecords,
        IPrescriptionRepository prescriptions) : IUnitOfWork
    {
        private readonly TelemedDbContext _context = context;

        public IPatientRepository Patients { get; } = patients;
        public IDoctorRepository Doctors { get; } = doctors;
        public IAppointmentRepository Appointments { get; } = appointments;
        public IMedicalRecordRepository MedicalRecords { get; } = medicalRecords;
        public IPrescriptionRepository Prescriptions { get; } = prescriptions;

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
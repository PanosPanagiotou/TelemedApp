using TelemedApp.Domain.Entities;

namespace TelemedApp.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Patient> Patients { get; }
        IRepository<Doctor> Doctors { get; }
        IRepository<Appointment> Appointments { get; }
        IRepository<MedicalRecord> MedicalRecords { get; }
        IRepository<Prescription> Prescriptions { get; }

        Task<int> SaveChangesAsync();
    }
}

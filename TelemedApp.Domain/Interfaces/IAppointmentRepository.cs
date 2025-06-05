using TelemedApp.Domain.Entities;

namespace TelemedApp.Domain.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment> 
    {
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
        Task<bool> HasConflictAsync(Guid doctorId, DateTime start, DateTime end);
        Task<bool> PatientHasConflictAsync(Guid patientId, DateTime start, DateTime end);
    }
}
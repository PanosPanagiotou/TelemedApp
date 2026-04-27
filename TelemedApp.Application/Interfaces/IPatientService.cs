using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(Guid id);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(Guid id);
    }
}
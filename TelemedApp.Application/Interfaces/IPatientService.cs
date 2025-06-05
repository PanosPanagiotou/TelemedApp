using TelemedApp.Application.DTOs;

namespace TelemedApp.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(Guid id);
        Task<bool> UpdatePatientAsync(PatientDto patientDto);
        Task<bool> DeletePatientAsync(Guid id);
    }
}

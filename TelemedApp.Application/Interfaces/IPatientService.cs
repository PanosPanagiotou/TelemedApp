using TelemedApp.Application.DTOs;

namespace TelemedApp.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);
        Task<PatientDto?> GetPatientByIdAsync(Guid id);
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    }
}

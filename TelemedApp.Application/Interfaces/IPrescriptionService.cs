using TelemedApp.Domain.Entities;

namespace TelemedApp.Application.Interfaces
{
    public interface IPrescriptionService
    {
        Task<Prescription> CreatePrescriptionAsync(Prescription prescription);
        Task<IEnumerable<Prescription>> GetPrescriptionsByRecordIdAsync(Guid recordId);
        Task<Prescription?> GetPrescriptionByIdAsync(Guid id);
        Task<bool> UpdatePrescriptionAsync(Prescription prescription);
        Task<bool> DeletePrescriptionAsync(Guid id);
    }
}
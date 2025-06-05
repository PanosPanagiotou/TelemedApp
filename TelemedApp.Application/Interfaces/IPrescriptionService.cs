using TelemedApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
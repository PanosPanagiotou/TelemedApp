using TelemedApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelemedApp.Application.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord record);
        Task<IEnumerable<MedicalRecord>> GetRecordsByPatientIdAsync(Guid patientId);
        Task<MedicalRecord?> GetRecordByIdAsync(Guid id);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecord record);
        Task<bool> DeleteMedicalRecordAsync(Guid id);
    }
}
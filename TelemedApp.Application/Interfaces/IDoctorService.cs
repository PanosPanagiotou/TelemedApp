using TelemedApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelemedApp.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(Guid id);
        Task<bool> UpdateDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(Guid id);
    }
}
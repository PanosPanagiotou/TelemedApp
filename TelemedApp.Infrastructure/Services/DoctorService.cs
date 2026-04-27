using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;

namespace TelemedApp.Infrastructure.Services
{
    public class DoctorService(IUnitOfWork uow) : IDoctorService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            await _uow.Doctors.AddAsync(doctor);
            await _uow.SaveChangesAsync();
            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _uow.Doctors.GetAllAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _uow.Doctors.GetAsync(id);
        }

        public async Task<bool> UpdateDoctorAsync(Doctor doctor)
        {
            var existing = await _uow.Doctors.GetAsync(doctor.Id);
            if (existing == null)
                return false;

            _uow.Doctors.Update(doctor);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDoctorAsync(Guid id)
        {
            var existing = await _uow.Doctors.GetAsync(id);
            if (existing == null)
                return false;

            await _uow.Doctors.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
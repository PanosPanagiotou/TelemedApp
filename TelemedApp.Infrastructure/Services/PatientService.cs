using TelemedApp.Application.Interfaces;
using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;

namespace TelemedApp.Infrastructure.Services
{
    public class PatientService(IUnitOfWork uow) : IPatientService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            await _uow.Patients.AddAsync(patient);
            await _uow.SaveChangesAsync();
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _uow.Patients.GetAllAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(Guid id)
        {
            return await _uow.Patients.GetAsync(id);
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            var existing = await _uow.Patients.GetAsync(patient.Id);
            if (existing == null)
                return false;

            _uow.Patients.Update(patient);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePatientAsync(Guid id)
        {
            var existing = await _uow.Patients.GetAsync(id);
            if (existing == null)
                return false;

            await _uow.Patients.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
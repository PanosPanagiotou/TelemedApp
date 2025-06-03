using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastracture.Data;

namespace TelemedApp.Infrastracture.Repositories
{
    public class PatientRepository(TelemedDbContext context) : GenericRepository<Patient>(context), IPatientRepository
    {

        // Add specific methods if needed
    }
}

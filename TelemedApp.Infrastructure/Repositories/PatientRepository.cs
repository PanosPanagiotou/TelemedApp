using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastructure.Data;

namespace TelemedApp.Infrastructure.Repositories
{
    public class PatientRepository(TelemedDbContext context) : GenericRepository<Patient>(context), IPatientRepository
    {

        // Add specific methods if needed
    }
}
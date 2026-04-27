using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastructure.Data;

namespace TelemedApp.Infrastructure.Repositories
{
    public class DoctorRepository(TelemedDbContext context) : GenericRepository<Doctor>(context), IDoctorRepository
    {
    }
}
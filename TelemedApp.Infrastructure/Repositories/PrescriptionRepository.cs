using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastructure.Data;

namespace TelemedApp.Infrastructure.Repositories
{
    public class PrescriptionRepository(TelemedDbContext context) : GenericRepository<Prescription>(context), IPrescriptionRepository
    {
    }
}
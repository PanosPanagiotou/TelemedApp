using TelemedApp.Domain.Entities;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastructure.Data;

namespace TelemedApp.Infrastructure.Repositories
{
    public class MedicalRecordRepository(TelemedDbContext context) : GenericRepository<MedicalRecord>(context), IMedicalRecordRepository
    {
    }
}
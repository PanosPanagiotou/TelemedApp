using Microsoft.EntityFrameworkCore;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastracture.Data;

namespace TelemedApp.Infrastracture.Repositories
{
    public class GenericRepository<T>(TelemedDbContext context) : IRepository<T> where T : class
    {
        protected readonly TelemedDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public virtual void Update(T entity) => _dbSet.Update(entity);

        public virtual void Delete(T entity) => _dbSet.Remove(entity);
        
    }
}
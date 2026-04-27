using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TelemedApp.Domain.Interfaces;
using TelemedApp.Infrastructure.Data;

namespace TelemedApp.Infrastructure.Repositories
{
    public class GenericRepository<T>(TelemedDbContext context) : IRepository<T> where T : class
    {
        protected readonly TelemedDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public virtual async Task<T?> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
    }
}
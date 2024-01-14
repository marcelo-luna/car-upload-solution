using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FineData.Persistence.Repositories
{
    public class FineDataRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public FineDataRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
           return Task.FromResult(_context.Entry(entity).State = EntityState.Modified);
        }

        public Task Delete(T entity)
        {
          return Task.FromResult(_dbSet.Remove(entity));
        }

        public async Task AddBulkAsync(IEnumerable<T> entity)
        {
           await _dbSet.AddRangeAsync(entity);
        }
    }
}

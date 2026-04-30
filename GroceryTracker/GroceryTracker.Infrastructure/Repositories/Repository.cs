using GroceryTracker.Domain.Interfaces;
using GroceryTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryTracker.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        // Date Tracking Implementation
        public async Task<IEnumerable<T>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            // This assumes T has a DateAdded property. For strict Generic implementation, 
            // we usually use a Specification pattern, but here we cast for simplicity 
            // or rely on the specific implementation in GroceryItemRepository.
            if (typeof(T) == typeof(GroceryTracker.Domain.Entities.GroceryItem))
            {
                var query = _dbSet as IQueryable<Domain.Entities.GroceryItem>;
                return (IEnumerable<T>)await query!
                    .Where(i => i.DateAdded >= start && i.DateAdded <= end)
                    .ToListAsync();
            }
            return await _dbSet.ToListAsync();
        }

        public void Update(T entity) => _dbSet.Update(entity);
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using GroceryTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryTracker.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<GroceryItem> GroceryItems { get; }
        Task<int> CompleteAsync();
    }

    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
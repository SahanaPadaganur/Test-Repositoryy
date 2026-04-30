using System;
using System.Collections.Generic;
using System.Text;

using GroceryTracker.Domain.Interfaces;
using GroceryTracker.Infrastructure.Data;

namespace GroceryTracker.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            GroceryItems = new Repository<Domain.Entities.GroceryItem>(_context);
        }

        public IRepository<Domain.Entities.GroceryItem> GroceryItems { get; }

        public async System.Threading.Tasks.Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
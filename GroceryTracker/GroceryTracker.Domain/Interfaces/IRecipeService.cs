using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryTracker.Domain.Interfaces
{
    public interface IRecipeService
    {
        // Business Logic: Resolve ingredients from a dish name
        Task<List<string>> GetIngredientsForDishAsync(string dishName);
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using GroceryTracker.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryTracker.Infrastructure.Services
{
    // In a real app, this would call an external API or scrape a recipe site.
    public class MockRecipeService : IRecipeService
    {
        public async Task<List<string>> GetIngredientsForDishAsync(string dishName)
        {
            // Simulating async processing
            await Task.Delay(100);

            return dishName.ToLower() switch
            {
                "spaghetti bolognese" => new List<string> { "Spaghetti Pasta", "Minced Beef", "Tomato Sauce", "Onion", "Garlic" },
                "pancakes" => new List<string> { "Flour", "Milk", "Eggs", "Baking Powder", "Sugar" },
                "caesar salad" => new List<string> { "Romaine Lettuce", "Croutons", "Parmesan Cheese", "Caesar Dressing", "Chicken Breast" },
                _ => new List<string>() // Unknown dish
            };
        }
    }
}
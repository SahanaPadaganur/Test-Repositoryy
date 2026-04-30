using System;
using System.Collections.Generic;
using System.Text;

using GroceryTracker.Application.DTOs;
using GroceryTracker.Domain.Entities;
using GroceryTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryTracker.Application.Services
{
    public class GroceryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeService _recipeService;

        public GroceryService(IUnitOfWork unitOfWork, IRecipeService recipeService)
        {
            _unitOfWork = unitOfWork;
            _recipeService = recipeService;
        }

        // Recipe Integration Logic
        public async Task AddFromRecipeAsync(string dishName, string userId)
        {
            var ingredients = await _recipeService.GetIngredientsForDishAsync(dishName);

            foreach (var ingredientName in ingredients)
            {
                // Avoid duplicates for the same user
                var exists = await _unitOfWork.GroceryItems.GetAllAsync();
                if (!exists.Any(i => i.Name == ingredientName && i.UserId == userId))
                {
                    var item = new GroceryItem
                    {
                        Name = ingredientName,
                        Quantity = "1 unit", // Default logic
                        UserId = userId,
                        DateAdded = DateTime.UtcNow
                    };
                    await _unitOfWork.GroceryItems.AddAsync(item);
                }
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<GroceryItemDto>> GetItemsByDateAsync(DateTime start, DateTime end, string userId)
        {
            var allItems = await _unitOfWork.GroceryItems.GetByDateRangeAsync(start, end);
            var userItems = allItems.Where(i => i.UserId == userId);

            return userItems.Select(i => new GroceryItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Quantity = i.Quantity,
                IsPurchased = i.IsPurchased,
                DateAdded = i.DateAdded
            });
        }

        public async Task<IEnumerable<GroceryItemDto>> GetAllItemsAsync(string userId)
        {
            var items = await _unitOfWork.GroceryItems.GetAllAsync();
            return items.Where(i => i.UserId == userId)
                        .Select(i => new GroceryItemDto { Id = i.Id, Name = i.Name, Quantity = i.Quantity, IsPurchased = i.IsPurchased, DateAdded = i.DateAdded });
        }
    }
}
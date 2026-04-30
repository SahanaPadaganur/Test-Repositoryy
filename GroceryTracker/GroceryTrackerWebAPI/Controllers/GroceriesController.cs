using Asp.Versioning;
using GroceryTracker.Domain.Entities;
using GroceryTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroceryTrackerWebAPI.Controllers
{
    [Authorize] // Requires a valid JWT token
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroceriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeService _recipeService;
        private readonly UserManager<User> _userManager;

        public GroceriesController(
            IUnitOfWork unitOfWork,
            IRecipeService recipeService,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _recipeService = recipeService;
            _userManager = userManager;
        }

        // 1. Core Logic: Add ingredients from a specific dish
        [HttpPost("add-from-dish")]
        public async Task<IActionResult> AddFromDish([FromBody] string dishName)
        {
            if (string.IsNullOrWhiteSpace(dishName))
                return BadRequest("Dish name cannot be empty.");

            // Get the ID of the currently logged-in user from the JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            // Resolve the dish into a list of ingredients
            var ingredients = await _recipeService.GetIngredientsForDishAsync(dishName);

            if (ingredients == null || !ingredients.Any())
                return NotFound($"Could not find ingredients for dish: {dishName}");

            var addedItems = new List<GroceryItem>();

            // Create and add a GroceryItem for each ingredient
            foreach (var ingredient in ingredients)
            {
                var groceryItem = new GroceryItem
                {
                    Name = ingredient,
                    Quantity = "1", // Default quantity, can be expanded later
                    IsPurchased = false,
                    DateAdded = DateTime.UtcNow,
                    UserId = userId
                };

                await _unitOfWork.GroceryItems.AddAsync(groceryItem);
                addedItems.Add(groceryItem);
            }

            // Save all new items to the database in a single transaction
            await _unitOfWork.CompleteAsync();

            return Ok(new { Message = $"Added {addedItems.Count} ingredients for {dishName}", Items = addedItems });
        }

        // 2. Date Tracking: Get grocery list with optional date filtering
        [HttpGet]
        public async Task<IActionResult> GetMyGroceries([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Note: In a production app, you would add a method to your repository 
            // like GetByUserIdAsync to filter out other users' data at the database level!
            var allItems = await _unitOfWork.GroceryItems.GetAllAsync();
            var userItems = allItems.Where(i => i.UserId == userId).AsQueryable();

            if (startDate.HasValue)
                userItems = userItems.Where(i => i.DateAdded >= startDate.Value);

            if (endDate.HasValue)
                userItems = userItems.Where(i => i.DateAdded <= endDate.Value);

            // Sort by DateAdded descending (newest first)
            var sortedList = userItems.OrderByDescending(i => i.DateAdded).ToList();

            return Ok(sortedList);
        }
    }
}
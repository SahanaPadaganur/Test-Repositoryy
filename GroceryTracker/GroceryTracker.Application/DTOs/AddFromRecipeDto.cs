using System.ComponentModel.DataAnnotations;

namespace GroceryTracker.Application.DTOs
{
    public class AddFromRecipeDto
    {
        [Required]
        public string DishName { get; set; } = string.Empty;
    }
}
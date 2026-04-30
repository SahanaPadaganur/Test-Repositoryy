using System;

namespace GroceryTracker.Application.DTOs
{
    public class GroceryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public bool IsPurchased { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
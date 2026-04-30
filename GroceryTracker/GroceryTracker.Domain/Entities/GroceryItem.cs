using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace GroceryTracker.Domain.Entities
{
    public class GroceryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public bool IsPurchased { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        // Foreign key to User
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}

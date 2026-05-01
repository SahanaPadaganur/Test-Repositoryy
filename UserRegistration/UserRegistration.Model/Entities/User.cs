using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistration.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

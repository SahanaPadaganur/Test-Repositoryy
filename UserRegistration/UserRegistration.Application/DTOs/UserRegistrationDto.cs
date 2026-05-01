using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserRegistration.Application.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

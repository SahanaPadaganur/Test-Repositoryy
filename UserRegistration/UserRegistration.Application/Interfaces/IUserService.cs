using System;
using System.Collections.Generic;
using System.Text;
using UserRegistration.Model.Entities;
using UserRegistration.Application.DTOs;
namespace UserRegistration.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> RegisterUserAsync(UserRegistrationDto userDto);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UserRegistrationDto updatedUserDto);
        Task<bool> DeleteUserAsync(int id);
    }
}

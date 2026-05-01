using Microsoft.EntityFrameworkCore;
using UserRegistration.Application.DTOs;
using UserRegistration.Application.Interfaces;
using UserRegistration.Infrastructure.Data;
using UserRegistration.Model.Entities;

namespace UserRegistration.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> RegisterUserAsync(UserRegistrationDto userDto)
        {
            // Business Logic: Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Name.ToLower() == userDto.Name.ToLower()))
            {
                return null; // Return null to indicate failure due to existing user
            }

            var newUser = new User
            {
                Name = userDto.Name,
                Password = userDto.Password // Remember to hash this in production!
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> UpdateUserAsync(int id, UserRegistrationDto updatedUserDto)
        {
            var userToUpdate = await _context.Users.FindAsync(id);

            if (userToUpdate == null) return false;

            userToUpdate.Name = updatedUserDto.Name;
            userToUpdate.Password = updatedUserDto.Password;

            _context.Entry(userToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
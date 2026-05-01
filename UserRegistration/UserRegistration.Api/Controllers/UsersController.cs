using Microsoft.AspNetCore.Mvc;
using UserRegistration.Application.DTOs;
using UserRegistration.Application.Interfaces;
using UserRegistration.Model.Entities;

namespace UserRegistration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegistrationDto userDto)
        {
            var newUser = await _userService.RegisterUserAsync(userDto);
            if (newUser == null) return BadRequest("Username is already taken.");

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserRegistrationDto updatedUserDto)
        {
            if (!await _userService.UpdateUserAsync(id, updatedUserDto)) return NotFound("User not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!await _userService.DeleteUserAsync(id)) return NotFound("User not found.");
            return NoContent();
        }
    }
}
using Server.Data;
using Server.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;

namespace Server.Endpoints
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly BonsaiContext _context;

        public UserController(BonsaiContext context)
        {
            _context = context;
        }

        // GET: api/users/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // POST: api/users
        public async Task<ActionResult<User>> CreateUser([FromBody] UserRegistrationDTO dto)
        {
            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Returns a 201 Created response with a Location header pointing to the GET endpoint for this user.
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // Helper method to hash passwords using SHA256
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
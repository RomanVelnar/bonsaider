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


//
// namespace Server.Endpoints
// {
//     public static class UserEndpoints
//     {
//         // Extension method to register user-related endpoints
//         public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
//         {
//             // POST /users endpoint to register a new user
//             app.MapPost("/users", async (BonsaiContext db, UserRegistrationDTO dto) =>
//             {
//                 // Create a new user using the provided DTO data
//                 var newUser = new User
//                 {
//                     Username = dto.Username,
//                     Email = dto.Email,
//                     PasswordHash = HashPassword(dto.Password)
//                 };
//
//                 // Add the new user to the context and save changes
//                 db.Users.Add(newUser);
//                 await db.SaveChangesAsync();
//
//                 // Return a Created result with a location and some user data
//                 return Results.Created($"/users/{newUser.Id}", new
//                 {
//                     Message = "User created successfully!",
//                     newUser.Username,
//                     newUser.Email
//                 });
//             });
//
//             return app;
//         }
//
//         // Helper method to hash passwords using SHA256
//         private static string HashPassword(string password)
//         {
//             using var sha256 = SHA256.Create();
//             var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//             return Convert.ToBase64String(bytes);
//         }
//     }
// }
using Server.Data;
using Server.Models;
using System.Security.Cryptography;
using System.Text;
using Server.DTOs;

namespace Server.Endpoints
{
    public static class UserEndpoints
    {
        // Extension method to register user-related endpoints
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            // POST /users endpoint to register a new user
            app.MapPost("/users", async (BonsaiContext db, UserRegistrationDTO dto) =>
            {
                // Create a new user using the provided DTO data
                var newUser = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    PasswordHash = HashPassword(dto.Password)
                };

                // Add the new user to the context and save changes
                db.Users.Add(newUser);
                await db.SaveChangesAsync();

                // Return a Created result with a location and some user data
                return Results.Created($"/users/{newUser.Id}", new
                {
                    Message = "User created successfully!",
                    newUser.Username,
                    newUser.Email
                });
            });

            return app;
        }

        // Helper method to hash passwords using SHA256
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
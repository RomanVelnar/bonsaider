using System.Security.Cryptography;
using System.Text;
using Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register services like DbContext (if needed) here in the future.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Add a simple example to demonstrate creating a User object
app.MapGet("/", () =>
{
    // Example: Creating a new user
    var user = new User
    {
        Username = "testuser",
        Email = "test@example.com",
        PasswordHash = HashPassword("password123") // Hashing the password
    };

    return new
    {
        Message = "User created successfully!",
        Username = user.Username,
        Email = user.Email,
        PasswordHash = user.PasswordHash
    };
});

app.Run();

// Helper method to hash the password using SHA256
static string HashPassword(string password)
{
    using (var sha256 = SHA256.Create())
    {
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}

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

app.MapGet("/", () =>
{
    var user = new User
    {
        Username = "testuser",
        Email = "test@example.com",
        PasswordHash = HashPassword("password123")
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

static string HashPassword(string password)
{
    using (var sha256 = SHA256.Create())
    {
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}

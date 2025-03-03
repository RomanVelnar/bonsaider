using System.Security.Cryptography;
using System.Text;
using Server.Models;
using Server.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables (if using .env)
DotNetEnv.Env.Load();

// Retrieve connection string from appsettings.json or environment variables
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost"};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT") ?? "5433"};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME") ?? "bonsaiderDB"};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER") ?? "postgres"};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASS") ?? "Password123!"};";


// Register DbContext with PostgreSQL
builder.Services.AddDbContext<BonsaiContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Apply pending migrations (Ensures DB schema is up-to-date)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BonsaiContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Test API Endpoint to create a user
app.MapPost("/users", async (BonsaiContext db, User user) =>
{
    // Example: Creating a new user
    var newUser = new User
    {
        Username = "testuser",
        Email = "test@example.com",
        PasswordHash = HashPassword("password123!") // Hashing the password
    };

    return Results.Created($"/users/{user.Id}", new
    {
        Message = "User created successfully!",
        Username = user.Username,
        Email = user.Email
    });
});

// Root endpoint
app.MapGet("/", () => "PostgreSQL is connected successfully!");

app.Run();

static string HashPassword(string password)
{
    using var sha256 = SHA256.Create();
    var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    return Convert.ToBase64String(bytes);
}
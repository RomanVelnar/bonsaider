using Microsoft.EntityFrameworkCore;
using Server.Data;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Configure your connection string and register your DbContext
var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASS")};";

builder.Services.AddDbContext<BonsaiContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BonsaiContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.MapGet("/", () => "Server is up and running!");

app.Run();
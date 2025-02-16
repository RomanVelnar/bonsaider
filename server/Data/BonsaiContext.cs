using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class BonsaiContext : DbContext
{
    public BonsaiContext(DbContextOptions<BonsaiContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
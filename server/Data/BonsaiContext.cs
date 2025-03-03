using Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Data;

public class BonsaiContext : DbContext
{
    public BonsaiContext(DbContextOptions<BonsaiContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
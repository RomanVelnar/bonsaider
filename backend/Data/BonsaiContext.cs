using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class BonsaiContext : DbContext
    {
        public BonsaiContext(DbContextOptions<BonsaiContext> options) : base(options) { }

        // Define the Users table
        public DbSet<User> Users { get; set; }
    }

}
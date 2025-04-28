using DatingApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        // Add DbSet properties for your entities here
        // Example:
        public DbSet<AppUser> Users { get; set; }
        // public DbSet<User> Users { get; set; }
    }
}

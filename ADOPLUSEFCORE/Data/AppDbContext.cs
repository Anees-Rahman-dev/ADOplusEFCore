using ADOPLUSEFCORE.models;
using Microsoft.EntityFrameworkCore;

namespace ADOPLUSEFCORE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Players> Players { get; set; }
    }
}


using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class WebApplicationDBContext : DbContext
    {
        public WebApplicationDBContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(o => o.Name);
        }
    }
}

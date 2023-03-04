using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Entities;

namespace YourTutor.Infrastructure.DAL
{
    internal sealed class YourTutorDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public YourTutorDbContext(DbContextOptions<YourTutorDbContext> dbContextOptions) : base (dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}



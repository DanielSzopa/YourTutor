using Microsoft.EntityFrameworkCore;
using YourTutor.Core.Entities;

namespace YourTutor.Infrastructure.DAL
{
    internal sealed class YourTutorDbContext : DbContext
    {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Tutor> Tutor { get; set; }
        internal DbSet<Offer> Offers { get; set; }

        public YourTutorDbContext(DbContextOptions<YourTutorDbContext> dbContextOptions) : base (dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.HasDefaultSchema(ConstantsDAL.DefaultSchema);
        }
    }
}



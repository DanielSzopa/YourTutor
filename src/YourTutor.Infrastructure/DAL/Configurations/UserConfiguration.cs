using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourTutor.Core.Entities;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Infrastructure.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id)
                .HasConversion(u => u.Value, u => new UserId(u));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasConversion(e => e.Value, e => new Core.ValueObjects.Email(e));

            builder.Property(u => u.FirstName)
               .HasConversion(f => f.Value, f => new FirstName(f));

            builder.Property(u => u.LastName)
               .HasConversion(l => l.Value, l => new LastName(l));

            builder.Property(u => u.HashPassword)
               .HasConversion(h => h.Value, h => new HashPassword(h));
        }
    }
}



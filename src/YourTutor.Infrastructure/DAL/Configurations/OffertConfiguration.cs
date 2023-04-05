using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourTutor.Core.Entities;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Infrastructure.DAL.Configurations;

public class OffertConfiguration : IEntityTypeConfiguration<Offert>
{
    public void Configure(EntityTypeBuilder<Offert> builder)
    {
        builder.Property(o => o.Id)
            .HasConversion(o => o.Value, o => new OffertId(o));

        builder.Property(o => o.Price)
            .HasConversion(p => p.Value, p => new Price(p));

        builder.Property(o => o.TutorId)
            .HasConversion(t => t.Value, t => new UserId(t));
    }
}



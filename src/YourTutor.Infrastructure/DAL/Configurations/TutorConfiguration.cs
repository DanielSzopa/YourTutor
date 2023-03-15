using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourTutor.Core.Entities;
using YourTutor.Core.ValueObjects;

namespace YourTutor.Infrastructure.DAL.Configurations
{
    public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> builder)
        {
            builder.Property(t => t.TutorId)
                .HasConversion(t => t.Value, t => new TutorId(t));

            builder.Property(t => t.UserId)
                .HasConversion(t => t.Value, t => new UserId(t));

        }
    }
}



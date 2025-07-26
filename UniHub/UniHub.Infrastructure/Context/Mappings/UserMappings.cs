using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniHub.Domain.Entities;

namespace UniHub.Infrastructure.Context.Mappings;

public class UserMappings : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.ClerkId)
            .IsRequired()
            .HasMaxLength(50);
    }
}

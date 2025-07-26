using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniHub.Domain.Entities;

namespace UniHub.Infrastructure.Context.Mappings
{
    internal class CourseMappings : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Code)
                .HasMaxLength(10);

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey("AdminId")
                  .IsRequired(false);

            entity.Ignore(e => e.Members);
        }
    }
}

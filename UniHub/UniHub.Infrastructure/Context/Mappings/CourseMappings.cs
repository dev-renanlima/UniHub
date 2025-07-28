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

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey("UserId")
                  .IsRequired(false);
        }
    }
}

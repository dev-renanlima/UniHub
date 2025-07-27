using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniHub.Domain.Entities;

namespace UniHub.Infrastructure.Context.Mappings
{
    public class CourseMemberMappings : IEntityTypeConfiguration<CourseMember>
    {
        public void Configure(EntityTypeBuilder<CourseMember> builder)
        {
            builder.HasKey(cm => new { cm.CourseId, cm.UserId });

            builder.HasOne(cm => cm.Course)
                   .WithMany(c => c.CourseMembers)
                   .HasForeignKey(cm => cm.CourseId);

            builder.HasOne(cm => cm.User)
                   .WithMany(u => u.Courses)
                   .HasForeignKey(cm => cm.UserId);
        }
    }
}
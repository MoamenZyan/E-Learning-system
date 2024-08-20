using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning_Platform_API.Domain.Entities.EntityConfiguration
{
    public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.ToTable("Lectures");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.VideoPath)
                        .HasColumnType("VARCHAR")
                        .HasMaxLength(256)
                        .IsRequired();

            // Relations
            builder.HasOne(x => x.Course)
                .WithMany(x => x.CourseLectures)
                .HasForeignKey(x => x.CourseId);
        }
    }
}

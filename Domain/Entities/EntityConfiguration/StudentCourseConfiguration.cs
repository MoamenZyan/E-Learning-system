using E_Learning_Platform_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning_Platform_API.Domain.Entities.EntityConfiguration
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.ToTable("StudentCourses");
            builder.HasKey(x => new { x.StudentId, x.CourseId });

            builder.Property(x => x.Progress)
                .HasColumnType("INTEGER")
                .IsRequired();

            builder.Property(x => x.EntrollDate)
                    .HasColumnType("DateTime")
                    .IsRequired();

            // Relations
            builder.HasOne(x => x.Student)
                .WithMany(x => x.EnrolledCourses)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Course)
                    .WithMany(x => x.EnrolledStudents)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

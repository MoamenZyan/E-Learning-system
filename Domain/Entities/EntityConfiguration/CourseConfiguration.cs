using E_Learning_Platform_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning_Platform_API.Domain.Entities.EntityConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(255)
                    .IsRequired();

            builder.Property(x => x.Description)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(255)
                    .IsRequired();


            // Relations
            builder.HasOne(x => x.Instructor)
                    .WithMany(x => x.OwnedCourses)
                    .HasForeignKey(x => x.InstructorId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

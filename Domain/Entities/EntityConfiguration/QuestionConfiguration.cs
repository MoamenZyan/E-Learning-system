using E_Learning_Platform_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning_Platform_API.Domain.Entities.EntityConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Description)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(1024)
                    .IsRequired();

            builder.Property(x => x.Answer)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(1024)
                    .IsRequired();

            // Relations
            builder.HasOne(x => x.Course)
                .WithMany(x => x.CourseQuestions)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

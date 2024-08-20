using E_Learning_Platform_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning_Platform_API.Domain.Entities.EntityConfiguration
{
    public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder.ToTable("ExamQuestions");
            builder.HasKey(x => new { x.ExamId, x.QuestionId });

            // Relations
            builder.HasOne(x => x.Exam)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                    .WithMany(x => x.Exams)
                    .HasForeignKey(x => x.QuestionId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

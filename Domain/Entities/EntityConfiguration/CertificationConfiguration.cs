using E_Learning_Platform_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning_Platform_API.Domain.Entities.EntityConfiguration
{
    public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable("Certifications");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.IssueDate)
                    .HasColumnType("DateTime")
                    .IsRequired();

            // Relations
            builder.HasOne(x => x.Student)
                    .WithMany(x => x.OwnedCertifications)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Course)
                    .WithMany(x => x.Certifications)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

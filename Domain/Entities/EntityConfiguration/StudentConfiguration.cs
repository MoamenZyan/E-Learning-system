using E_Learning_Platform_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning_Platform_API.Domain.Entities.EntityConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Fname)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(144)
                    .IsRequired();

            builder.Property(x => x.Lname)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(144)
                    .IsRequired();

            builder.Property(x => x.Password)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(255)
                    .IsRequired();

            builder.Property(x => x.Email)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(144)
                    .IsRequired();

            builder.Property(x => x.Phone)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(16)
                    .IsRequired();
        }
    }
}

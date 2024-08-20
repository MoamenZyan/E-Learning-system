using Microsoft.EntityFrameworkCore;
using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Entities.EntityConfiguration;

namespace E_Learning_Platform_API.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        // Database Context
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply Entities Configurations
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorConfiguration());
            modelBuilder.ApplyConfiguration(new ExamConfiguration());
            modelBuilder.ApplyConfiguration(new CertificationConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());
            modelBuilder.ApplyConfiguration(new ExamQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new LectureConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

using E_Learning_Platform_API.Domain.Factories;

namespace E_Learning_Platform_API.Domain.Entities
{
    public class Certification
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }

    public class CertificationMinimalDto
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }

        public StudentMinimalDto Student { get; set; } = null!;
        public CourseMinimalDto Course { get; set; } = null!;

        public CertificationMinimalDto(Certification certification)
        {
            Id = certification.Id;
            IssueDate = certification.IssueDate;

            Student = StudentFactory.CreateStudentMinimalDto(certification.Student);
            Course = CourseFactory.CreateCourseMinimalDto(certification.Course);
        }
    }
}

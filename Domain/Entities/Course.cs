using E_Learning_Platform_API.Domain.Factories;

namespace E_Learning_Platform_API.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int InstructorId { get; set; }

        public IEnumerable<Certification> Certifications { get; set; } = new List<Certification>();
        public Instructor Instructor { get; set; } = null!;
        public IEnumerable<StudentCourse> EnrolledStudents { get; set; } = new List<StudentCourse>();
        public IEnumerable<Exam> CourseExams { get; set; } = new List<Exam>();
        public IEnumerable<Question> CourseQuestions { get; set; } = new List<Question>();
        public IEnumerable<Lecture> CourseLectures { get; set; } = new List<Lecture>();
    }

    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public InstructorMinimalDto Instructor { get; set; } = null!;
        public IEnumerable<StudentMinimalDto> EnrolledStudents { get; set; } = new List<StudentMinimalDto>();
        public IEnumerable<ExamMinimalDto> CourseExams { get; set; } = new List<ExamMinimalDto>();
        public IEnumerable<QuestionMinimalDto> CourseQuestions { get; set; } = new List<QuestionMinimalDto>();

        public CourseDto(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Description = course.Description;
            Instructor = InstructorFactory.CreateInstructorMinimalDto(course.Instructor);
            CourseQuestions = course.CourseQuestions.Select(x => QuestionFactory.CreateQuestionMinimalDto(x)).ToList();
            CourseExams = course.CourseExams.Select(x => ExamFactory.CreateExamMinimalDto(x)).ToList();
            EnrolledStudents = course.EnrolledStudents.Select(x => StudentFactory.CreateStudentMinimalDto(x.Student)).ToList();
        }
    }
    public class CourseMinimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public InstructorMinimalDto Instructor { get; set; } = null!;

        public CourseMinimalDto(Course course)
        {
            Id = course.Id;
            Name = course.Name;
            Description = course.Description;

            Instructor = InstructorFactory.CreateInstructorMinimalDto(course.Instructor);
        }
    }

}

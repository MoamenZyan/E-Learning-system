using E_Learning_Platform_API.Domain.Factories;

namespace E_Learning_Platform_API.Domain.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public int CourseId { get; set; }


        public Course Course { get; set; } = null!;
        public IEnumerable<ExamQuestion> Questions { get; set; } = new List<ExamQuestion>();
    }

    public class ExamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string CourseName { get; set; }


        public IEnumerable<QuestionMinimalDto> Questions { get; set; } = new List<QuestionMinimalDto>();

        public ExamDto(Exam exam)
        {
            Id = exam.Id;
            Name = exam.Name;
            Duration = exam.Duration;
            CourseName = exam.Course.Name;

            Questions = exam.Questions.Select(x => QuestionFactory.CreateQuestionMinimalDto(x.Question)).ToList();
        }

    }

    public class ExamMinimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string CourseName { get; set; }

        public ExamMinimalDto(Exam exam)
        {
            Id = exam.Id;
            Name = exam.Name;
            Duration = exam.Duration;

            CourseName = exam.Course.Name;
        }

    }
}

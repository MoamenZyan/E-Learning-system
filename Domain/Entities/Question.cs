namespace E_Learning_Platform_API.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public required string Answer { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;
        public IEnumerable<ExamQuestion> Exams { get; set; } = new List<ExamQuestion>();
    }

    public class QuestionMinimalDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public QuestionMinimalDto(Question question)
        {
            Id = question.Id;
            Description = question.Description;
        }
    }

}

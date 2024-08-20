namespace E_Learning_Platform_API.Domain.Entities
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Progress { get; set; }
        public DateTime EntrollDate { get; set; }


        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}

namespace E_Learning_Platform_API.Domain.Entities
{
    public class Lecture
    {
        public int Id { get; set; }
        public string VideoPath { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }

    public class LectureMinimalDto
    {
        public int Id { get; set; }
        public string VideoPath { get; set; }
        public int CourseId { get; set; }

        public LectureMinimalDto(Lecture lecture)
        {
            Id = lecture.Id;
            VideoPath = lecture.VideoPath;
            VideoPath = lecture.VideoPath;
            CourseId = lecture.CourseId;
        }
    }
}

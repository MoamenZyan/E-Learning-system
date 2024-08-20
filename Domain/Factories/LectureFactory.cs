using E_Learning_Platform_API.Domain.Entities;
using Ganss.Xss;

namespace E_Learning_Platform_API.Domain.Factories
{
    public class LectureFactory
    {
        // Create Lecture
        public static Lecture CreateLecture(FileInfo file, int courseId)
        {
            Lecture lecture = new Lecture
            {
               CourseId = courseId,
               VideoPath = file.FullName,
            };
            return lecture;
        }

        public static LectureMinimalDto CreateLectureMinimalDto(Lecture lecture)
        {
            return new LectureMinimalDto(lecture);
        }
    }
}

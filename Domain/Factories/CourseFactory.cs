using Ganss.Xss;
using Microsoft.Extensions.Primitives;
using E_Learning_Platform_API.Domain.Entities;

namespace E_Learning_Platform_API.Domain.Factories
{
    public class CourseFactory
    {

        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Course CreateCourse(Dictionary<string, StringValues> body)
        {
            Course course = new Course()
            {
                Name = sanitizer.Sanitize(body["name"]!),
                Description = sanitizer.Sanitize(body["Description"]!),
                InstructorId = Convert.ToInt32(body["InstructorId"]),
            };
            return course;
        }

        public static CourseDto CreateCourseDto(Course course)
        {
            return new CourseDto(course);
        }

        public static CourseMinimalDto CreateCourseMinimalDto(Course course)
        {
            return new CourseMinimalDto(course);
        }
    }
}

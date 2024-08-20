using E_Learning_Platform_API.Domain.Entities;

namespace E_Learning_Platform_API.Domain.Factories
{
    public class StudentCourseFactory
    {
        public static StudentCourse CreateStudentCourse(int studentId, int courseId)
        {
            StudentCourse studentCourse = new StudentCourse()
            {
                StudentId = studentId,
                CourseId = courseId,
                Progress = 0,
                EntrollDate = DateTime.Now,
            };
            return studentCourse;
        }
    }
}

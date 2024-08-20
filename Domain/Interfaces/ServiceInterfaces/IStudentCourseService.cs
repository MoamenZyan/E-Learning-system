using E_Learning_Platform_API.Domain.Entities;

namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface IStudentCourseService
    {
        Task<StudentCourse?> AddStudentToCourse(int studentId, int courseId);
        Task<bool> RemoveStudentFromCourse(int studentId, int courseId);
        Task<StudentCourse?> GetStudentCourse(int studentId, int courseId);
        Task<bool> UpdateStudentCourse(StudentCourse studentCourse);
    }
}

using E_Learning_Platform_API.Domain.Entities;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface ICourseService
    {
        Task<Course?> CreateCourse(Dictionary<string, StringValues> body, int InstructorId);
        Task<bool> DeleteCourse(int courseId);
        Task<bool> UpdateCourse(int courseId, Dictionary<string, StringValues> body);
        Task<Course?> GetCourseById(int courseId);
    }
}

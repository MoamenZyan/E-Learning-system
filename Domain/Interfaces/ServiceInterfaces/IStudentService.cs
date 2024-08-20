using E_Learning_Platform_API.Domain.Entities;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface IStudentService
    {
        Task<Student?> GetStudentById(int studentId);
        (bool, int) CheckStudentCredentials(UserCredentials userCredentials);
        Task<Student?> CreateStudent(Dictionary<string, StringValues> body);
        Task<bool> DeleteStudent(int studentId);
    }
}

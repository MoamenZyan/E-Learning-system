using E_Learning_Platform_API.Domain.Entities;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface IInstructorService
    {
        (bool, int) CheckInstructorCredentials(UserCredentials userCredentials);
        Task<Instructor?> GetInstructorById(int instructorId);
        Task<Instructor?> CreateInstructor(Dictionary<string, StringValues> body);
        Task<bool> DeleteInstructor(int instructorId);
    }
}

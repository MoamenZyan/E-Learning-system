using E_Learning_Platform_API.Domain.Entities;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface IExamService
    {
        Task<bool> CreateNewExam(IFormCollection body, int courseId);
        Task<bool> UpdateExam(Dictionary<string, StringValues> body, int examId);
        Task<bool> DeleteExam(int examId);
        Task<Exam?> GetExamById(int examId);
    }
}

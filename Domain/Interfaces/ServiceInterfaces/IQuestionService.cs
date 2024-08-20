using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface IQuestionService
    {
        Task<bool> AddNewQuestionToCourse(IFormCollection body, int courseId);
        Task<bool> RemoveQuestionFromCourse(int questionId);
        Task<bool> AddQuestionToExam(int questionId, int examId);
        Task<bool> RemoveQuestionFromExam(int questionId, int examId);
    }
}

using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;

namespace E_Learning_Platform_API.Application
{
    public class QuestionApplicationService
    {
        private readonly IQuestionService _questionService;
        public QuestionApplicationService(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<bool> CreateNewQuestion (IFormCollection body, int courseId)
        {
            var result = await _questionService.AddNewQuestionToCourse(body, courseId);
            return result;
        }
        public async Task<bool> AddQuestionToExam(int questionId, int examId)
        {
            var result = await _questionService.AddQuestionToExam(questionId, examId);
            return result;
        }
        public async Task<bool> DeleteQuestionFromExam(int questionId, int examId)
        {
            var result = await _questionService.RemoveQuestionFromExam(questionId, examId);
            return result;
        }
        public async Task<bool> DeleteQuestionFromCourse(int questionId)
        {
            var result = await _questionService.RemoveQuestionFromCourse(questionId);
            return result;
        }
    }
}

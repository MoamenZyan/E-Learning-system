using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Services.EntitiesServices
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IJunctionTableRepository<ExamQuestion> _examQuestionRepository;
        public QuestionService(IRepository<Question> questionRepository, IJunctionTableRepository<ExamQuestion> examQuestionRepository)
        {
            _questionRepository = questionRepository;
            _examQuestionRepository = examQuestionRepository;
        }
        // Create new question
        public async Task<bool> AddNewQuestionToCourse(IFormCollection body, int courseId)
        {
            Question question = QuestionFactory.CreateQuestion(body, courseId);
            try
            {
                await _questionRepository.AddAsync(question);
                await _questionRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        // Add question to exam
        public async Task<bool> AddQuestionToExam(int questionId, int examId)
        {
            ExamQuestion examQuestion = ExamQuestionFactory.CreateExamQuestion(examId, questionId);
            try
            {
                await _examQuestionRepository.AddAsync(examQuestion);
                await _examQuestionRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }
        
        // Remove question from course
        public async Task<bool> RemoveQuestionFromCourse(int questionId)
        {
            Question? question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
                return true;

            try
            {
                _questionRepository.DeleteAsync(question);
                await _questionRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { 
                Console.Write(ex.ToString());
                return false;
            }
        }

        // Remove question from exam
        public async Task<bool> RemoveQuestionFromExam(int questionId, int examId)
        {
            ExamQuestion? examQuestion = await _examQuestionRepository.GetByIdAsync(examId, questionId);
            if (examQuestion == null) 
                return true;
            try
            {
                _examQuestionRepository.DeleteAsync(examQuestion);
                await _examQuestionRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }
    }
}

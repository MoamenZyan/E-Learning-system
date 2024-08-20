using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Services.EntitiesServices
{
    public class ExamService : IExamService
    {
        private readonly IRepository<Exam> _examRepository;
        public ExamService(IRepository<Exam> examRepository)
        {
            _examRepository = examRepository;
        }
        // Create new exam
        public async Task<bool> CreateNewExam(IFormCollection body, int courseId)
        {
            try
            {
                Exam exam = ExamFactory.CreateExam(body, courseId);
                await _examRepository.AddAsync(exam);
                await _examRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        // Delete exam from database
        public async Task<bool> DeleteExam(int examId)
        {
            Exam? exam = await _examRepository.GetByIdAsync(examId);
            if (exam == null)
                return true;
            try
            {
                _examRepository.DeleteAsync(exam);
                await _examRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        // Get exam by id
        public async Task<Exam?> GetExamById(int examId)
        {
            return await _examRepository.GetByIdAsync(examId);
        }

        // Update exam
        public async Task<bool> UpdateExam(Dictionary<string, StringValues> body, int examId)
        {
            Exam? exam = await _examRepository.GetByIdAsync(examId);
            if (exam == null)
                return false;

            try
            {
                exam.Duration = new TimeSpan(Convert.ToInt16(body["ExamHour"]), 0, 0);
                exam.Name = body["Name"]!;
                _examRepository.UpdateAsync(exam);
                await _examRepository.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

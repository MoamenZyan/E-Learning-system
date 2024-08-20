using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.NotificationInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using E_Learning_Platform_API.Infrastructure.Configurations;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices.EmailServiceStrategies;
using E_Learning_Platform_API.Utils;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace E_Learning_Platform_API.Application
{
    public class ExamApplicationService
    {
        private readonly IExamService _examService;
        private readonly IInstructorService _instructorService;
        private readonly NotificationContext _notificationContext;
        private readonly ICourseService _courseService;
        private readonly SendGridSettings _sendGridSettings;
        private readonly IRedisService _redisService;

        public ExamApplicationService(IExamService examService,
                            IInstructorService instructorService, 
                            NotificationContext notificationContext,
                            ICourseService courseService,
                            IOptions<SendGridSettings> sendGridSettings,
                            IRedisService redisService)
        {
            _examService = examService;
            _instructorService = instructorService; 
            _notificationContext = notificationContext;
            _courseService = courseService;
            _sendGridSettings = sendGridSettings.Value;
            _redisService = redisService;
        }

        // Get exam by id
        public async Task<ExamDto?> GetExamById(int examId)
        {
            var fromRedis = await _redisService.Get($"exam:{examId}");
            if ( fromRedis == null )
            {
                var result = await _examService.GetExamById(examId);
                if (result == null)
                    return null;

                await _redisService.Set($"exam:{result.Id}", JsonConvert.SerializeObject(result, JsonSettings.JsonSerializerSettings), new TimeSpan(0, 15, 0));
                return ExamFactory.CreateExamDto(result);
            }
            else
            {
                return ExamFactory.CreateExamDto(JsonConvert.DeserializeObject<Exam>(fromRedis, JsonSettings.JsonSerializerSettings)!);
            }
        }


        // Create new exam
        public async Task<bool> CreateNewExam(IFormCollection body, int instructorId, int courseId)
        {
            Instructor? instructor = await _instructorService.GetInstructorById(instructorId);
            Course? course = await _courseService.GetCourseById(courseId);
            if (instructor == null || course == null)
                return false;

            var result = await _examService.CreateNewExam(body, courseId);
            if (result == false)
                return false;


            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("CourseName", course.Name);

            _notificationContext.SetNotificationServiceStrategy(new InstructorCreatedExamEmailStrategy(_sendGridSettings));
            await _notificationContext.SendNotification(instructor.Fname, instructor.Email, dict);
            return true;
        }

        // Delete exam
        public async Task<bool> DeleteExam(int examId)
        {
            var result = await _examService.DeleteExam(examId);
            await _redisService.Del($"exam:{examId}");
            return result;
        }
    }
}

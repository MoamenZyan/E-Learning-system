using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using E_Learning_Platform_API.Infrastructure.Configurations;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices.EmailServiceStrategies;
using E_Learning_Platform_API.Utils;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace E_Learning_Platform_API.ApplicationServices
{
    public class StudentApplicationService
    {
        private readonly IStudentService _studentService;
        private readonly NotificationContext _notificationContext;
        private readonly SendGridSettings _sendGridSettings;
        private readonly JwtHelper _jwtHelper;
        private readonly IRedisService _redisService;
        public StudentApplicationService(IStudentService studentService,
                NotificationContext notificationContext,
                IOptions<SendGridSettings> sendGridSettings,
                JwtHelper jwtHelper,
                IRedisService redisService)
        {
            _studentService = studentService;
            _notificationContext = notificationContext;
            _sendGridSettings = sendGridSettings.Value;
            _jwtHelper = jwtHelper;
            _redisService = redisService;
        }

        // Create new student & Send notification
        public async Task<StudentMinimalDto?> CreateNewStudent(Dictionary<string, StringValues> body)
        {
            Student? student = await _studentService.CreateStudent(body);
            if (student == null)
                return null;

            _notificationContext.SetNotificationServiceStrategy(new WelcomeEmailStrategy(_sendGridSettings));
            await _notificationContext.SendNotification(student.Fname, student.Email, null!);

            return StudentFactory.CreateStudentMinimalDto(student);
        }

        // Student login
        public string StudentLogin(int userId)
        {
            var token = _jwtHelper.GenerateToken(userId);
            return token;
        }

        public async Task<StudentDto?> GetStudentById(int id)
        {
            var fromRedis = await _redisService.Get($"student:{id}");
            if (fromRedis == null)
            {
                var student = await _studentService.GetStudentById(id);
                if (student == null)
                    return null;
                await _redisService.Set($"student:{id}", JsonConvert.SerializeObject(student, JsonSettings.JsonSerializerSettings), new TimeSpan(0, 15, 0));

                return StudentFactory.CreateStudentDto(student);
            }
            else
            {
                return StudentFactory.CreateStudentDto(JsonConvert.DeserializeObject<Student>(fromRedis, JsonSettings.JsonSerializerSettings)!);

            }
        }
    }
}

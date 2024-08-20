using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using E_Learning_Platform_API.Domain.Services.EntitiesServices;
using E_Learning_Platform_API.Infrastructure.Configurations;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices.EmailServiceStrategies;
using E_Learning_Platform_API.Utils;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace E_Learning_Platform_API.ApplicationServices
{
    public class InstructorApplicationService
    {
        private readonly IInstructorService _instructorService;
        private readonly NotificationContext _notificationContext;
        private readonly SendGridSettings _sendGridSettings;
        private readonly JwtHelper _jwtHelper;
        private readonly IRedisService _redisService;
        public InstructorApplicationService(IInstructorService instructorService, 
                        NotificationContext notificationContext,
                        IOptions<SendGridSettings> sendGridSettings,
                        JwtHelper jwtHelper,
                        IRedisService redisService)
        {
            _instructorService = instructorService;
            _notificationContext = notificationContext;
            _sendGridSettings = sendGridSettings.Value;
            _jwtHelper = jwtHelper;
            _redisService = redisService;
        }

        public string InstructorLogin(int userId)
        {
            var token = _jwtHelper.GenerateToken(userId);
            return token;
        }

        // Method to create instructor
        public async Task<bool> CreateInstructor(Dictionary<string, StringValues> body)
        {
            var result = await _instructorService.CreateInstructor(body);
            if (result == null)
                return false;

            _notificationContext.SetNotificationServiceStrategy(new WelcomeEmailStrategy(_sendGridSettings));
            await _notificationContext.SendNotification(result.Fname, result.Email, null!);

            return true;
        }

        // Get instructor by his id
        public async Task<InstructorDto?> GetInstructorById(int id)
        {
            var fromRedis = await _redisService.Get($"instructor:{id}");
            if (fromRedis == null)
            {
                var instructor = await _instructorService.GetInstructorById(id);
                if (instructor == null)
                    return null;

                await _redisService.Set($"course:{id}", JsonConvert.SerializeObject(instructor, JsonSettings.JsonSerializerSettings), new TimeSpan(0, 15, 0));
                return InstructorFactory.CreateInstructorDto(instructor);
            }
            else
            {
                return InstructorFactory.CreateInstructorDto(JsonConvert.DeserializeObject<Instructor>(fromRedis, JsonSettings.JsonSerializerSettings)!);
            }
            
        }
    }
}

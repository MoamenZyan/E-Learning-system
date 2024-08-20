using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.NotificationInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using E_Learning_Platform_API.Infrastructure.Configurations;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices.EmailServiceStrategies;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using E_Learning_Platform_API.Utils;

namespace E_Learning_Platform_API.Application
{
    public class CourseApplicationService
    {
        private readonly IStudentCourseService _studentCourseService;
        private readonly ICourseService _courseService;
        private readonly NotificationContext _notificationContext;
        private readonly SendGridSettings _sendGridSettings;
        private readonly IStudentService _studentService;
        private readonly ICertificationService _certificationService;
        private readonly IRedisService _redisService;
        public CourseApplicationService(IStudentCourseService studentCourseService,
                    ICourseService courseService,
                    NotificationContext notificationContext,
                    IOptions<SendGridSettings> sendGridSettings,
                    IStudentService studentService,
                    ICertificationService certificationService,
                    IRedisService redisService)
        {
            _studentCourseService = studentCourseService;
            _courseService = courseService;
            _notificationContext = notificationContext;
            _sendGridSettings = sendGridSettings.Value;
            _studentService = studentService;
            _certificationService = certificationService;
            _redisService = redisService;
        }


        // Create new course
        public async Task<bool> CreateCourse(Dictionary<string, StringValues> body, int InstructorId)
        {
            var result = await _courseService.CreateCourse(body, InstructorId);
            if (result == null)
                return false;

            return true;
        }

        // Add student to course
        public async Task<bool> AddStudentToCourse(int studentId, int courseId)
        {
            var result = await _studentCourseService.AddStudentToCourse(studentId, courseId);
            if (result == null)
                return false;

            _notificationContext.SetNotificationServiceStrategy(new StudentAddedToCourseEmail(_sendGridSettings));

            Course? course = await _courseService.GetCourseById(courseId);
            Student? student = await _studentService.GetStudentById(studentId);
            if (course == null || student == null)
                return false;

            var body = new Dictionary<string, string>();
            body.Add("CourseName", course.Name);
            body.Add("InstructorName", course.Instructor.Fname);

            await _notificationContext.SendNotification(student.Fname, student.Email, body);

            await _redisService.Set($"course:{courseId}", JsonConvert.SerializeObject(course, JsonSettings.JsonSerializerSettings), new TimeSpan(0, 15, 0));

            return true;
        }

        // Delete student from course
        public async Task<bool> RemoveStudentFromCourse(int studentId, int courseId)
        {
            var result = await _studentCourseService.RemoveStudentFromCourse(studentId, courseId);
            await _redisService.Del($"course:{courseId}");
            return false;
        }

        // Get course by id
        public async Task<CourseDto?> GetCourseById(int id)
        {
            var fromRedis = await _redisService.Get($"course:{id}");
            if (fromRedis == null)
            {
                Course? course = await _courseService.GetCourseById(id);
                if (course == null)
                    return null;

                var result = await _redisService.Set($"course:{course.Id}", JsonConvert.SerializeObject(course, JsonSettings.JsonSerializerSettings), new TimeSpan(0, 15, 0));
                if (result)
                    return CourseFactory.CreateCourseDto(course);
                else
                    return null;
            }
            else
            { 
                return CourseFactory.CreateCourseDto(JsonConvert.DeserializeObject<Course>(fromRedis, JsonSettings.JsonSerializerSettings)!);
            }
        }

        // Mocking increasing progress in course
        // Increase course progress
        public async Task<bool> IncreaseCourseProgress(int studentId, int courseId)
        {
            var studentCourse = await _studentCourseService.GetStudentCourse(studentId, courseId);
            if (studentCourse == null)
                return false;

            if (studentCourse.Progress < 100)
            {
                studentCourse.Progress += 20;
                await _studentCourseService.UpdateStudentCourse(studentCourse);

                if (studentCourse.Progress >= 100)
                {
                    await _certificationService.CreateCertification(studentId, courseId);
                    _notificationContext.SetNotificationServiceStrategy(new GotCertificationEmailStrategy(_sendGridSettings));

                    Dictionary<string, string> body = new Dictionary<string, string>();
                    body.Add("CourseName", studentCourse.Course.Name);

                    await _notificationContext.SendNotification(studentCourse.Student.Fname, studentCourse.Student.Email, body);
                }
            }
            await _redisService.Set($"course:{studentCourse.CourseId}", JsonConvert.SerializeObject(studentCourse.Course, JsonSettings.JsonSerializerSettings), new TimeSpan(0, 15, 0));
            return true;
        }
    }
}

using E_Learning_Platform_API.Application;
using E_Learning_Platform_API.ApplicationServices;
using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using E_Learning_Platform_API.Domain.Services.EntitiesServices;
using E_Learning_Platform_API.Filters.Authentication;
using E_Learning_Platform_API.Filters.Authorization;
using E_Learning_Platform_API.Infrastructure.Configurations;
using E_Learning_Platform_API.Infrastructure.Data;
using E_Learning_Platform_API.Infrastructure.Repositories;
using E_Learning_Platform_API.Infrastructure.Services.FileSystemRepositoryService;
using E_Learning_Platform_API.Infrastructure.Services.NotificationServices;
using E_Learning_Platform_API.Infrastructure.Services.RedisServices;
using E_Learning_Platform_API.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Learning_Platform_API.Infrastructure.IoC
{
    // Register Services and Dependencies
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
            JwtBearerSettings jwtBearerSettings = new JwtBearerSettings();
            configuration.GetSection("JwtOptions").Bind(jwtBearerSettings);
            JwtAuthenticationOptions jwtAuthenticationOptions = new JwtAuthenticationOptions(jwtBearerSettings);
            services.AddSingleton(jwtBearerSettings);
            services.AddScoped<JwtHelper>();
            services.AddSingleton<SendGridSettings>();


            services.AddScoped<IRepository<Certification>, CertificationRepository>();
            services.AddScoped<IRepository<Instructor>, InstructorRepository>();
            services.AddScoped<IRepository<Student>, StudentRepository>();
            services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IRepository<Exam>, ExamRepository>();
            services.AddScoped<IRepository<Question>, QuestionRepository>();
            services.AddScoped<IJunctionTableRepository<StudentCourse>, StudentCourseRepository>();
            services.AddScoped<IRepository<Lecture>, LectureRepository>();
            services.AddScoped<IJunctionTableRepository<ExamQuestion>, ExamQuestionRepository>();

            services.AddScoped<IRedisService, RedisService>();

            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<ICertificationService, CertificationService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IStudentCourseService, StudentCourseService>();
            services.AddScoped<ICourseService, CourseService>();


            services.AddScoped<NotificationContext>();

            services.AddScoped<LectureApplicationService>();
            services.AddScoped<CourseApplicationService>();
            services.AddScoped<StudentApplicationService>();
            services.AddScoped<InstructorApplicationService>();
            services.AddScoped<CertificationApplicationService>();
            services.AddScoped<ExamApplicationService>();
            services.AddScoped<QuestionApplicationService>();

            services.AddScoped<OwnerOfCourseAuthorization>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                jwtAuthenticationOptions.ConfigureJwtBearerOptions(options));

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
            
            return services;
        }
    }
}

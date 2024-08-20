using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Services.EntitiesServices
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Course?> CreateCourse(Dictionary<string, StringValues> body, int InstructorId)
        {
            body.Add("InstructorId", InstructorId.ToString());
            Course course = CourseFactory.CreateCourse(body);
            try
            {
                await _courseRepository.AddAsync(course);
                await _courseRepository.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            Course? course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                return true;
            try
            {
                _courseRepository.DeleteAsync(course);
                await _courseRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<Course?> GetCourseById(int courseId)
        {
            Course? course = await _courseRepository.GetByIdAsync(courseId);
            return course;
        }

        public async Task<bool> UpdateCourse(int courseId, Dictionary<string, StringValues> body)
        {
            Course? course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                return true;
            try
            {
                course.Description = body["Description"]!;
                course.Name = body["Name"]!;
                _courseRepository.UpdateAsync(course);
                await _courseRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

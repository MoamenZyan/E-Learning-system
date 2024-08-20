using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;

namespace E_Learning_Platform_API.Domain.Services.EntitiesServices
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IJunctionTableRepository<StudentCourse> _studentCourseRepository;
        public StudentCourseService(IJunctionTableRepository<StudentCourse> studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }
        public async Task<StudentCourse?> AddStudentToCourse(int studentId, int courseId)
        {
            StudentCourse studentCourse = StudentCourseFactory.CreateStudentCourse(studentId, courseId);
            try
            {
                await _studentCourseRepository.AddAsync(studentCourse);
                await _studentCourseRepository.SaveChanges();
                return studentCourse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null!;
            }
        }

        public async Task<StudentCourse?> GetStudentCourse(int studentId, int courseId)
        {
            return await _studentCourseRepository.GetByIdAsync(studentId, courseId);
        }

        public async Task<bool> RemoveStudentFromCourse(int studentId, int courseId)
        {
            StudentCourse? studentCourse = _studentCourseRepository.GetByIdAsync(courseId, studentId).Result;
            if (studentCourse == null)
                return true;
            try
            {
                _studentCourseRepository.DeleteAsync(studentCourse);
                await _studentCourseRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateStudentCourse(StudentCourse studentCourse)
        {
            var result = _studentCourseRepository.UpdateAsync(studentCourse);
            await _studentCourseRepository.SaveChanges();
            return result;
        }
    }
}

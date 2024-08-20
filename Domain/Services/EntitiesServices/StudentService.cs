using BCrypt.Net;
using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Services.EntitiesServices
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // Checks student's credentials
        public (bool, int) CheckStudentCredentials(UserCredentials userCredentials)
        {
            var student = _studentRepository.Filter(x => x.Email == userCredentials.Email)!.FirstOrDefault();
            if (student == null)
                return (false, 0);

            if (BCrypt.Net.BCrypt.Verify(userCredentials.Password, student.Password))
                return (true, student.Id);
            return (false, 0);
        }

        // Create new student
        public async Task<Student?> CreateStudent(Dictionary<string, StringValues> body)
        {

            try
            {
                var student = StudentFactory.CreateStudent(body);
                await _studentRepository.AddAsync(student);
                await _studentRepository.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Get student by his id
        public async Task<Student?> GetStudentById(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            return student;
        }

        // Delete student
        public async Task<bool> DeleteStudent(int studentId)
        {
            var student = await GetStudentById(studentId);
            if (student == null) return true;
            var result = _studentRepository.DeleteAsync(student);
            if (result)
                await _studentRepository.SaveChanges();
            
            return result;
        }
    }
}

using BCrypt.Net;
using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Primitives;

namespace E_Learning_Platform_API.Domain.Services.EntitiesServices
{
    public class InstructorService : IInstructorService
    {
        private readonly IRepository<Instructor> _instructorRepository;

        public InstructorService(IRepository<Instructor> instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        // Checks instructor's credentials
        public (bool, int) CheckInstructorCredentials(UserCredentials userCredentials)
        {
            Instructor instructor = _instructorRepository.Filter(x => x.Email == userCredentials.Email)!.FirstOrDefault()!;
            if (instructor == null)
                return (false, 0);
            if (BCrypt.Net.BCrypt.Verify(userCredentials.Password, instructor.Password))
                return (true, instructor.Id);
            return (false, 0);
        }

        // Create new instructor
        public async Task<Instructor?> CreateInstructor(Dictionary<string, StringValues> body)
        {
            var instructor = InstructorFactory.CreateInstructor(body);
            try
            {
                await _instructorRepository.AddAsync(instructor);
                await _instructorRepository.SaveChanges();
                return instructor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Delete instructor
        public async Task<bool> DeleteInstructor(int instructorId)
        {
            var instructor = await GetInstructorById(instructorId);
            if (instructor == null) return true;
            var result = _instructorRepository.DeleteAsync(instructor);
            if (result)
                await _instructorRepository.SaveChanges();
            return result;
        }

        // Get instructor by his id
        public async Task<Instructor?> GetInstructorById(int instructorId)
        {
            var instructor = await _instructorRepository.GetByIdAsync(instructorId);
            return instructor;
        }
    }
}

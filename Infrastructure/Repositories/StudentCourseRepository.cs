using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class StudentCourseRepository : IJunctionTableRepository<StudentCourse>
    {
        private readonly DatabaseContext _context;
        public StudentCourseRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(StudentCourse entity)
        {
            try
            {
                await _context.StudentCourses.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(StudentCourse entity)
        {
            try
            {
                _context.StudentCourses.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<StudentCourse>?> GetAllAsync(int id)
        {
            return await _context.StudentCourses.Where(x => x.CourseId == id).ToListAsync();
        }

        public async Task<StudentCourse?> GetByIdAsync(int id1, int id2)
        {
            return await _context.StudentCourses
                            .Include(x => x.Student)
                            .Include(x => x.Course)
                            .FirstOrDefaultAsync(x => x.StudentId == id1 && x.CourseId == id2);
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool UpdateAsync(StudentCourse entity)
        {
            try
            {
                _context.StudentCourses.Update(entity);
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

using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly DatabaseContext _context;
        public CourseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Course entity)
        {
            try
            {
                await _context.Courses.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(Course entity)
        {
            try
            {
                _context.Courses.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Course>? Filter(Func<Course, bool> filter)
        {
            return _context.Courses.Where(filter);
        }

        public async Task<IEnumerable<Course>?> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                        .Include(x => x.Instructor)
                        .Include(x => x.EnrolledStudents)
                            .ThenInclude(x => x.Student)
                        .Include(x => x.CourseLectures)
                        .FirstOrDefaultAsync(x => x.Id == id);
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

        public bool UpdateAsync(Course entity)
        {
            try
            {
                _context.Courses.Update(entity);
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

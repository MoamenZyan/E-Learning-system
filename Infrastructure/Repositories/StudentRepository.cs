using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DatabaseContext _context;
        public StudentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Student entity)
        {
            try
            {
                await _context.Students.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(Student entity)
        {
            try
            {
                _context.Students.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Student>? Filter(Func<Student, bool> filter)
        {
            return _context.Students.Where(filter);
        }

        public async Task<IEnumerable<Student>?> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
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

        public bool UpdateAsync(Student entity)
        {
            try
            {
                _context.Students.Update(entity);
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

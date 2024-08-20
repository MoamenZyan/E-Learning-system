using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class InstructorRepository : IRepository<Instructor>
    {
        private readonly DatabaseContext _context;
        public InstructorRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Instructor entity)
        {
            try
            {
                await _context.Instructors.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(Instructor entity)
        {
            try
            {
                _context.Instructors.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Instructor>? Filter(Func<Instructor, bool> filter)
        {
            return _context.Instructors.Where(filter);
        }

        public async Task<IEnumerable<Instructor>?> GetAllAsync()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task<Instructor?> GetByIdAsync(int id)
        {
            return await _context.Instructors.FirstOrDefaultAsync(x => x.Id == id);
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

        public bool UpdateAsync(Instructor entity)
        {
            try
            {
                _context.Instructors.Update(entity);
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

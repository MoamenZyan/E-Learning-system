using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class LectureRepository : IRepository<Lecture>
    {
        private readonly DatabaseContext _context;
        public LectureRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Lecture entity)
        {
            try
            {
                await _context.Lectures.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public  bool DeleteAsync(Lecture entity)
        {
            try
            {
                _context.Lectures.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public IEnumerable<Lecture>? Filter(Func<Lecture, bool> filter)
        {
            return _context.Lectures.Where(filter).ToList();
        }

        public async Task<IEnumerable<Lecture>?> GetAllAsync()
        {
            return await _context.Lectures.ToListAsync();
        }

        public async Task<Lecture?> GetByIdAsync(int id)
        {
            return await _context.Lectures.FirstOrDefaultAsync(x => x.Id == id);
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateAsync(Lecture entity)
        {
            try
            {
                _context.Lectures.Update(entity);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

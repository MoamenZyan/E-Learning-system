using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly DatabaseContext _context;
        public QuestionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Question entity)
        {
            try
            {
                await _context.Questions.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(Question entity)
        {
            try
            {
                _context.Questions.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Question>? Filter(Func<Question, bool> filter)
        {
            return _context.Questions.Where(filter);
        }

        public async Task<IEnumerable<Question>?> GetAllAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            return await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
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

        public bool UpdateAsync(Question entity)
        {
            try
            {
                _context.Questions.Update(entity);
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

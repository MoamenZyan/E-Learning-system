using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class ExamRepository : IRepository<Exam>
    {
        private readonly DatabaseContext _context;
        public ExamRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Exam entity)
        {
            try
            {
                await _context.Exams.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(Exam entity)
        {
            try
            {
                _context.Exams.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Exam>? Filter(Func<Exam, bool> filter)
        {
            return _context.Exams.Where(filter);
        }

        public async Task<IEnumerable<Exam>?> GetAllAsync()
        {
            return await _context.Exams.ToListAsync();
        }

        public async Task<Exam?> GetByIdAsync(int id)
        {
            return await _context.Exams
                                    .Include(x => x.Questions)
                                        .ThenInclude(x => x.Question)
                                    .Include(x => x.Course)
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

        public bool UpdateAsync(Exam entity)
        {
            try
            {
                _context.Exams.Update(entity);
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

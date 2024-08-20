using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class ExamQuestionRepository : IJunctionTableRepository<ExamQuestion>
    {
        private readonly DatabaseContext _context;
        public ExamQuestionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(ExamQuestion entity)
        {
            try
            {
                await _context.ExamQuestions.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(ExamQuestion entity)
        {
            try
            {
                _context.ExamQuestions.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<ExamQuestion>?> GetAllAsync(int id)
        {
            return await _context.ExamQuestions.Where(x => x.ExamId == id).ToListAsync();
        }

        public async Task<ExamQuestion?> GetByIdAsync(int id1, int id2)
        {
            return await _context.ExamQuestions.FirstOrDefaultAsync(x => x.ExamId == id1 && x.QuestionId == id2);
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

        public bool UpdateAsync(ExamQuestion entity)
        {
            try
            {
                _context.ExamQuestions.Update(entity);
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

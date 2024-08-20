using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Platform_API.Infrastructure.Repositories
{
    public class CertificationRepository : IRepository<Certification>
    {
        private readonly DatabaseContext _context;
        public CertificationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Certification entity)
        {
            try
            {
                await _context.Certifications.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool DeleteAsync(Certification entity)
        {
            try
            {
                _context.Certifications.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public IEnumerable<Certification>? Filter(Func<Certification, bool> filter)
        {
            return _context.Certifications
                            .Include(x => x.Course)
                                .ThenInclude(x => x.Instructor)
                            .Include(x => x.Student)
                            .Where(filter);
        }

        public async Task<IEnumerable<Certification>?> GetAllAsync()
        {
            return await _context.Certifications.ToListAsync();
        }

        public async Task<Certification?> GetByIdAsync(int id)
        {
            return await _context.Certifications.FirstOrDefaultAsync(x => x.Id == id);
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

        public bool UpdateAsync(Certification entity)
        {
            try
            {
                _context.Certifications.Update(entity);
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

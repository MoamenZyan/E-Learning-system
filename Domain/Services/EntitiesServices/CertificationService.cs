using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;

namespace E_Learning_Platform_API.Domain.Services.EntitiesServices
{
    public class CertificationService : ICertificationService
    {
        private readonly IRepository<Certification> _certificationRepository;
        public CertificationService(IRepository<Certification> certificationRepository)
        {
            _certificationRepository = certificationRepository;
        }
        public async Task<bool> CreateCertification(int studentId, int courseId)
        {
            Certification certification = CertificationFactory.CreateCertification(studentId, courseId);
            try
            {
                await _certificationRepository.AddAsync(certification);
                await _certificationRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCertification(int studentId, int courseId)
        {
            Certification? certification = _certificationRepository.Filter(x => x.StudentId == studentId && x.CourseId == courseId)!.FirstOrDefault()!;
            if (certification == null)
                return true;
            try
            {
                _certificationRepository.DeleteAsync(certification);
                await _certificationRepository.SaveChanges();
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

using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;

namespace E_Learning_Platform_API.Application
{
    public class CertificationApplicationService
    {
        private readonly IRepository<Certification> _certificationRepository;
        public CertificationApplicationService(IRepository<Certification> certificationRepository)
        {
            _certificationRepository = certificationRepository;
        }
        
        public  List<CertificationMinimalDto>? GetAllStudentCertifications(int studentId)
        {
            List<Certification>? certifications = _certificationRepository.Filter(x => x.StudentId == studentId)?.ToList();
            return certifications?.Select(x => CertificationFactory.CreateCertificationMinimalDto(x)).ToList();
        }
    }
}

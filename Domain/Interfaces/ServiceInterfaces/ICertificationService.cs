namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface ICertificationService
    {
        Task<bool> CreateCertification(int studentId, int courseId);
        Task<bool> DeleteCertification(int studentId, int courseId);
    }
}

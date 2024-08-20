namespace E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces
{
    public interface ILectureService
    {
        Task<FileInfo?> SaveVideo(IFormFile video, int courseId);
        Task<bool> LoadVideo(string videoPath);
        bool DeleteVideo(string videoPath); 
        string SanitizeVideo(string videoName);
    }
}

using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;

namespace E_Learning_Platform_API.Infrastructure.Services.FileSystemRepositoryService
{
    public class LectureService : ILectureService
    {

        public bool DeleteVideo(string videoPath)
        {
            if (File.Exists(videoPath))
            {
                try
                {
                    File.Delete(videoPath);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Task<bool> LoadVideo(string videoPath)
        {
            throw new NotImplementedException();
        }

        // sanitize video name
        public string SanitizeVideo(string videoName)
        {
            var sanitizedVideoName = videoName.Replace(" ", "_").Replace(":", "-");
            return sanitizedVideoName;
        }

        // Save video to server
        public async Task<FileInfo?> SaveVideo(IFormFile video, int courseId)
        {
            if (video == null || video.Length == 0 || courseId == default)
                return null;

            var directoryPath = Path.Combine("LecturesRepository", Convert.ToString(courseId));

            Directory.CreateDirectory(directoryPath);

            var sanitizedVideoName = SanitizeVideo(Path.GetFileNameWithoutExtension(video.FileName)) + Path.GetExtension(video.FileName);

            var filePath = Path.Combine($"LecturesRepository/{courseId}", sanitizedVideoName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }

                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}

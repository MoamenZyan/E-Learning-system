using E_Learning_Platform_API.Domain.Entities;
using E_Learning_Platform_API.Domain.Factories;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;

namespace E_Learning_Platform_API.Application
{
    public class LectureApplicationService
    {
        private readonly IRepository<Lecture> _lectureRepository;
        private readonly ILectureService _lectureService;
        public LectureApplicationService(IRepository<Lecture> lectureRepository, ILectureService lectureService)
        {
            _lectureRepository = lectureRepository;
            _lectureService = lectureService;
        }

        // To add video/lecture to course
        public async Task<bool> AddLecture(IFormFile formFile, int courseId)
        {
            var savingVideoResult = await _lectureService.SaveVideo(formFile, courseId);
            if (savingVideoResult == null)
                return false;
            var lecture = LectureFactory.CreateLecture(savingVideoResult, courseId);
            var savingVideoInDBResult = await _lectureRepository.AddAsync(lecture);
            return savingVideoInDBResult;
        }

        // To delete video/lecture from course
        public async Task<bool> DeleteLecture(int lectureId)
        {
            var lecture = await _lectureRepository.GetByIdAsync(lectureId);
            if (lecture == null) 
                return false;

            var result = _lectureService.DeleteVideo(lecture.VideoPath);
            if (result)
            {
                _lectureRepository.DeleteAsync(lecture);
                await _lectureRepository.SaveChanges();
            }
            return result;
        }
    }
}

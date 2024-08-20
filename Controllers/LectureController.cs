using E_Learning_Platform_API.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Learning_Platform_API.Filters.Authorization;

namespace E_Learning_Platform_API.Controllers
{
    [ApiController]
    [Route("/api/v1/lectures")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ServiceFilter(typeof(OwnerOfCourseAuthorization))]
    public class LecturesController : ControllerBase
    {
        private readonly LectureApplicationService _lectureApplicationService;
        public LecturesController(LectureApplicationService lectureApplicationService)
        {
            _lectureApplicationService = lectureApplicationService;
        }

        [HttpPost]
        [Route("{courseId}")]
        public async Task<IActionResult> AddLectureToCourse(int courseId, [FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest(new { status = false, message = "error in file" });
            
            var result = await _lectureApplicationService.AddLecture(file, courseId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in saving the video" }) { StatusCode = 500 };
            return Ok(new {status = true, message = "video saved and lecture added !"});
        }

        [HttpDelete]
        [Route("{courseId}/{lectureId}")]
        public async Task<IActionResult> DeleteLectureFromCourse(int lectureId, int courseId)
        {
            var result = await _lectureApplicationService.DeleteLecture(lectureId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in deleting lecture" }) { StatusCode = 500 };
            return Ok(new { status = true, message = "lecture deleted" });
        }
    }
}

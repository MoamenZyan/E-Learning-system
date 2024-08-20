using E_Learning_Platform_API.Application;
using E_Learning_Platform_API.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace E_Learning_Platform_API.Controllers
{
    [ApiController]
    [Route("/api/v1/courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoursesController : ControllerBase
    {
        private readonly CourseApplicationService _courseApplicationService;
        public CoursesController(CourseApplicationService courseApplicationService)
        {
            _courseApplicationService = courseApplicationService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            CourseDto? course = await _courseApplicationService.GetCourseById(id);
            if (course == null)
                return NotFound(new { status = false, message = "course doesn't exists" });
            return Ok(new {status=true, course = course});
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCourse()
        {
            var body = await new FormReader(Request.Body).ReadFormAsync();
            var userId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            var result = await _courseApplicationService.CreateCourse(body, userId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in creating course" }) { StatusCode = 500 };
            return Ok(new { status = true, message = "course has been created !" });
        }

        [HttpPost]
        [Route("student/{courseId}")]
        public async Task<IActionResult> AddStudentToCourse(int courseId)
        {
            var userId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            var result = await _courseApplicationService.AddStudentToCourse(userId, courseId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in adding student to course" }) { StatusCode = 500 };
            return Ok(new { status = true, message = "student added to course successfully !" });
        }

        [HttpDelete]
        [Route("student/{courseId}")]
        public async Task<IActionResult> DeleteStudentFromCourse(int courseId)
        {
            var userId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            var result = await _courseApplicationService.RemoveStudentFromCourse(userId, courseId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in removing student from course" }) { StatusCode = 500 };
            return Ok(new { status = true, message = "student removed from course successfully !" });
        }

        [HttpPost]
        [Route("progress-up")]
        public async Task<IActionResult> IncreaseCourseProgress()
        {
            var body = await new FormReader(Request.Body).ReadFormAsync();
            var userId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            var result = await _courseApplicationService.IncreaseCourseProgress(userId, Convert.ToInt32(body["CourseId"]));
            if (result == false)
                return new JsonResult(new { status = false, message = "error in increasing course progress" }) { StatusCode = 500 };
            return Ok(new { status = true, message = "course progress increased" });
        }
    }
}

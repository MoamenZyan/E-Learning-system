using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Learning_Platform_API.Filters.Authorization;
using E_Learning_Platform_API.Application;
using Microsoft.AspNetCore.WebUtilities;

namespace E_Learning_Platform_API.Controllers
{
    [ApiController]
    [Route("/api/v1/exams")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class ExamsController : ControllerBase
    {
        private readonly ExamApplicationService _examApplicationService;
        public ExamsController(ExamApplicationService examApplicationService)
        {
            _examApplicationService = examApplicationService;
        }

        [HttpPost]
        [Route("{courseId}")]
        [ServiceFilter(typeof(OwnerOfCourseAuthorization))]
        public async Task<IActionResult> CreateNewExam(int courseId)
        {
            var body = await Request.ReadFormAsync();
            if (body == null)
                return BadRequest(new { status = false, message = "body is null" });

            int userId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            var result = await _examApplicationService.CreateNewExam(body, userId, courseId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in creating exam" }) { StatusCode = 500 };

            return new JsonResult(new {status = true, message = "exam created successfully"});
        }

        [HttpDelete]
        [Route("{examId}/{courseId}")]
        [ServiceFilter(typeof(OwnerOfCourseAuthorization))]
        public async Task<IActionResult> DeleteExam(int examId, int courseId)
        {
            var result = await _examApplicationService.DeleteExam(examId);
            if (result == false)
                return new JsonResult(new {status = false, message = "error in deleting exam"}) { StatusCode = 500 };
            return new JsonResult(new { status = true, message = "exam deleted successfully" });
        }

        [HttpGet]
        [Route("{examId}")]
        public async Task<IActionResult> GetExamById(int examId)
        {
            var result = await _examApplicationService.GetExamById(examId);
            if (result == null)
                return new JsonResult(new { status = false, message = "exam not found" }) { StatusCode = 404 };
            return new JsonResult(new { status = true, exam = result});
        }
    }
}

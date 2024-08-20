using E_Learning_Platform_API.Application;
using E_Learning_Platform_API.Filters.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning_Platform_API.Controllers
{
    [ApiController]
    [Route("/api/v1/questions")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ServiceFilter(typeof(OwnerOfCourseAuthorization))]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionApplicationService _questionApplicationService;
        public QuestionsController(QuestionApplicationService questionApplicationService)
        {
            _questionApplicationService = questionApplicationService;
        }

        [HttpPost]
        [Route("{courseId}")]
        public async Task<IActionResult> CreateNewQuestion(int courseId)
        {
            var body = await Request.ReadFormAsync();
            var result = await _questionApplicationService.CreateNewQuestion(body, courseId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in creating question" }) { StatusCode = 500 };
            return new JsonResult(new { status = true, message = "question created successfully!" });
        }

        [HttpPost]
        [Route("{courseId}/exam")]
        public async Task<IActionResult> AddQuestionToExam(int courseId)
        {
            var body = await Request.ReadFormAsync();
            var result = await _questionApplicationService.AddQuestionToExam(Convert.ToInt32(body["QuestionId"]), Convert.ToInt32(body["ExamId"]));
            if (result == false)
                return new JsonResult(new { status = false, message = "error in adding question to exam" }) { StatusCode = 500 };
            return new JsonResult(new { status = true, message = "question added successfully!" });
        }

        [HttpDelete]
        [Route("{courseId}/exam")]
        public async Task<IActionResult> DeleteQuestionFromExam(int courseId)
        {
            var body = await Request.ReadFormAsync();
            var result = await _questionApplicationService.DeleteQuestionFromExam(Convert.ToInt32(body["QuestionId"]), courseId);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in deleting question from exam" }) { StatusCode = 500 };
            return new JsonResult(new { status = true, message = "question deleted successfully!" });
        }

        [HttpDelete]
        [Route("{courseId}")]
        public async Task<IActionResult> DeleteQuestionFromCourse(int courseId)
        {
            var body = await Request.ReadFormAsync();
            var result = await _questionApplicationService.DeleteQuestionFromCourse(Convert.ToInt32(body["QuestionId"]));
            if (result == false)
                return new JsonResult(new { status = false, message = "error in deleting question from course" }) { StatusCode = 500 };
            return new JsonResult(new { status = true, message = "question deleted successfully!" });
        }
    }
}

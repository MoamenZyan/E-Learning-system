using E_Learning_Platform_API.ApplicationServices;
using E_Learning_Platform_API.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace E_Learning_Platform_API.Controllers
{
    [ApiController]
    [Route("/api/v1/students")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentsController : ControllerBase
    {
        private readonly StudentApplicationService _studentApplicationService;
        public StudentsController(StudentApplicationService studentApplicationService)
        {
            _studentApplicationService = studentApplicationService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _studentApplicationService.GetStudentById(id);
            if (student == null)
                return NotFound(new { status = false, message = "student not found" });
            return new JsonResult(new { status = "true", student = student });
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNewStudent()
        {
            var body = await new FormReader(Request.Body).ReadFormAsync();
            var result = await _studentApplicationService.CreateNewStudent(body);
            return Ok(new {status = true, message = "student created !", student = result});
        }

        [HttpPost]
        [Route("login")]
        [Authorize(AuthenticationSchemes = "Basic")]
        public IActionResult StudentLogin()
        {
            var userId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            var token = _studentApplicationService.StudentLogin(userId);
            return Ok(new {status = true, token = token});
        }
    }
}


using E_Learning_Platform_API.ApplicationServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace E_Learning_Platform_API.Controllers
{
    [ApiController]
    [Route("/api/v1/instructors")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorApplicationService _instructorApplicationService;
        public InstructorsController(InstructorApplicationService instructorApplicationService)
        {
            _instructorApplicationService = instructorApplicationService;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateInstructor()
        {
            var body = await new FormReader(Request.Body).ReadFormAsync();
            var result = await _instructorApplicationService.CreateInstructor(body);
            if (result == false)
                return new JsonResult(new { status = false, message = "error in creating instructor" }) { StatusCode = 500 };
            return new JsonResult(new {status = true, message = "instructor created !"});
        }

        [HttpPost]
        [Route("login")]
        [Authorize(AuthenticationSchemes = "Basic")]
        public IActionResult InstructorLogin()
        {
            var userId = Convert.ToInt32(Request.HttpContext.User.Claims.First().Value);
            var token = _instructorApplicationService.InstructorLogin(userId);
            return new JsonResult(new { status = true, token = token });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            var instructor = await _instructorApplicationService.GetInstructorById(id);
            if (instructor == null)
                return NotFound(new { status = false, message = "instructor not found" });
            return Ok(new {status = true, instructor = instructor });
        }

    }
}

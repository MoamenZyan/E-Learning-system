using E_Learning_Platform_API.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning_Platform_API.Controllers
{
    [ApiController]
    [Route("/api/v1/certifications")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CertificationsController : ControllerBase
    {
        private readonly CertificationApplicationService _certificationApplicationService;
        public CertificationsController(CertificationApplicationService certificationApplicationService)
        {
            _certificationApplicationService = certificationApplicationService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAllStudentCertifications(int id)
        {
            var certifications = _certificationApplicationService.GetAllStudentCertifications(id);
            return Ok(new {status = true, certifications = certifications});
        }
    }
}

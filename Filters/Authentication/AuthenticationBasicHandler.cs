using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces;
using E_Learning_Platform_API.Domain.Entities;
using System.Net.Http.Headers;
using System.Text;
using E_Learning_Platform_API.Domain.Interfaces.ServiceInterfaces;
using System.Security.Claims;

namespace E_Learning_Platform_API.Filters.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IStudentService _studentService;

        private readonly IInstructorService _instructorService;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IStudentService studentService,
            IInstructorService instructorService) : base(options, logger, encoder, clock)
        {
            _instructorService = instructorService;
            _studentService = studentService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                !Request.Headers.ContainsKey("Type"))
                return Task.FromResult(AuthenticateResult.NoResult());

            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authHeader))
                return Task.FromResult(AuthenticateResult.Fail("Unknown Scheme"));

            if (!authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Unknown Scheme"));

            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter!)).Split(":");
            UserCredentials userCredentials = new UserCredentials()
            { 
                Email = credentials[0],
                Password = credentials[1]
            };

            var type = Request.Headers["Type"];

            var result = (default(bool), default(int));
            if (type == "instructor")
                result = _instructorService.CheckInstructorCredentials(userCredentials);
            else if (type == "student")
                result = _studentService.CheckStudentCredentials(userCredentials);
            else
                return Task.FromResult(AuthenticateResult.Fail("incorrect type header value"));

            if (result.Item1 is false)
                return Task.FromResult(AuthenticateResult.Fail("incorrect credentials"));

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim (ClaimTypes.NameIdentifier, Convert.ToString(result.Item2))
            }, authHeader.Scheme);

            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, authHeader.Scheme);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}

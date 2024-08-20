using E_Learning_Platform_API.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace E_Learning_Platform_API.Filters.Authentication
{
    public class JwtAuthenticationOptions
    {
        private readonly JwtBearerSettings _jwtBearerSettings;
        public JwtAuthenticationOptions(JwtBearerSettings jwtBearerSettings)
        {
            _jwtBearerSettings = jwtBearerSettings;
        }
        public void ConfigureJwtBearerOptions(JwtBearerOptions options)
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtBearerSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = _jwtBearerSettings.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSettings.SigningKey))
            };

            options.Events = new JwtBearerEvents()
            {
                OnTokenValidated = context =>
                {
                    var claims = context.Principal?.Identity as ClaimsIdentity;
                    if (claims != null)
                    {
                        var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        context.HttpContext.Items["userId"] = userId;
                    }
                    return Task.CompletedTask;
                }
            };
        }
    }
}

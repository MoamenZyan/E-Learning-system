using E_Learning_Platform_API.Infrastructure.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace E_Learning_Platform_API.Utils
{
    public class JwtHelper
    {
        private readonly JwtBearerSettings _jwtBearerSettings;
        public JwtHelper(JwtBearerSettings jwtBearerSettings)
        {
            _jwtBearerSettings = jwtBearerSettings;
        }

        // Generate JWT Bearer token
        public string GenerateToken(int userId)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtBearerSettings.Issuer,
                Audience = _jwtBearerSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSettings.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, Convert.ToString(userId)),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtBearerSettings.LifeTime)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

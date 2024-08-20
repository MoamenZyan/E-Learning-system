namespace E_Learning_Platform_API.Infrastructure.Configurations
{
    public class JwtBearerSettings
    {
        public string Audience { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string SigningKey { get; set; } = null!;
        public int LifeTime { get; set; }
    }
}

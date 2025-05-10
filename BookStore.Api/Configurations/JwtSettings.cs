namespace BookStore.Api.Configurations
{
    // This class holds JWT-related settings and is bound to configuration (appsettings.json).
    public class JwtSettings
    {
        // Symmetric secret key for signing the JWT
        public string SecretKey { get; set; } = "";

        // Issuer name (who issues the token)
        public string Issuer { get; set; } = "";

        // Audience name (who the token is intended for)
        public string Audience { get; set; } = "";

        // Token expiration duration in minutes
        public int ExpirationInMinutes { get; set; }
    }
}


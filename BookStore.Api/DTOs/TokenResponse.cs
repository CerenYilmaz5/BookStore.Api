namespace BookStore.Api.DTOs
{
    // This DTO represents the response sent to the client after a successful login.
    public class TokenResponse
    {
        // The JWT access token that should be included in Authorization headers.
        public string Token { get; set; } = "";

        // The username of the authenticated user.
        public string Username { get; set; } = "";

        // The role assigned to the user (e.g., "Fake" or "Normal").
        public string Role { get; set; } = "";

        // Token expiration time (in minutes or as a timestamp - optional).
        public int ExpiresIn { get; set; }
    }
}
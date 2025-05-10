namespace BookStore.Api.DTOs
{
    // This DTO represents the login request body
    public class UserLoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
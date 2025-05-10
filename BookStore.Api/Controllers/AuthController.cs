using BookStore.Api.DTOs;
using BookStore.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    // This controller handles authentication and token generation
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        // Inject the JWT service via constructor
        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        // POST: api/auth/login
        // Accepts a login request and returns a JWT token with additional user info
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            // Basic validation: both username and password must be provided
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { status = 400, message = "Username and password are required." });
            }

            // Determine role based on username (demo logic)
            string role = request.Username.ToLower() == "fake" ? "Fake" : "Normal";

            // Generate a JWT token for the user
            string token = _jwtService.GenerateToken(request.Username, role);

            // Return token response with user info
            var response = new TokenResponse
            {
                Token = token,
                Username = request.Username,
                Role = role,
                ExpiresIn = 60 // This should match JwtSettings.ExpirationInMinutes
            };

            return Ok(response);
        }
    }
}
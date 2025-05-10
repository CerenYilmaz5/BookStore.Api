using BookStore.Api.DTOs;
using BookStore.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    // Handles user login and JWT token issuance
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthenticationController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        // POST: api/authentication/login
        // Authenticates a user and returns a JWT token
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { status = 400, message = "Username and password are required." });
            }

            // Demo logic for role assignment
            string role = request.Username.ToLower() == "fake" ? "Fake" : "Normal";

            string token = _jwtService.GenerateToken(request.Username, role);

            var response = new TokenResponse
            {
                Token = token,
                Username = request.Username,
                Role = role,
                ExpiresIn = 60
            };

            return Ok(response);
        }
    }
}
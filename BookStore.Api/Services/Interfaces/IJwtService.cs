namespace BookStore.Api.Services.Interfaces
{
    // This interface defines the contract for JWT token generation.
    // It abstracts away the implementation details of how tokens are created.
    public interface IJwtService
    {
        // Generates a JWT token based on the given username and role.
        string GenerateToken(string username, string role);
    }
}
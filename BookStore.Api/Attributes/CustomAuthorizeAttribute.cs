using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Api.Attributes
{
    // This custom attribute is used to simulate user login control.
    // It checks the Authorization header for a specific token (like 'fake-token').
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _allowedToken;

        // Constructor takes in the required token string
        public CustomAuthorizeAttribute(string allowedToken)
        {
            _allowedToken = allowedToken;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (token != _allowedToken)
            {
                // Reject the request if token is missing or incorrect
                context.Result = new UnauthorizedObjectResult(new
                {
                    status = 401,
                    message = $"Access denied. Required token: '{_allowedToken}'."
                });
            }
        }
    }
}
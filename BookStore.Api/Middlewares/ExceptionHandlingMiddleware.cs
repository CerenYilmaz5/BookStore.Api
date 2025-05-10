using System.Net;
using System.Text.Json;

namespace BookStore.Api.Middlewares
{
    // This middleware catches unhandled exceptions globally
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Continue pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return standardized error response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = 500,
                    message = "Internal Server Error",
                    detail = ex.Message
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
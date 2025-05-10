using System.Net;
using System.Text.Json;

namespace BookStore.Api.Middlewares
{
    // This middleware catches any unhandled exceptions in the pipeline
    // and returns a standardized error response.
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
                // Proceed to next middleware/component
                await _next(context);
            }
            catch (Exception ex)
            {
                // If any exception is thrown, handle it here
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
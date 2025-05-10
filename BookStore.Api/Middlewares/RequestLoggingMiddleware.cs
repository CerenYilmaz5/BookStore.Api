namespace BookStore.Api.Middlewares
{
    // This middleware logs incoming requests to the console
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log basic request details
            Console.WriteLine($"[LOG] {context.Request.Method} {context.Request.Path}");

            // Continue pipeline
            await _next(context);
        }
    }
}
namespace BookStore.Api.Middlewares
{
    // This middleware logs basic details of incoming HTTP requests to the console.
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log the HTTP method and request path
            Console.WriteLine($"[LOG] {context.Request.Method} {context.Request.Path}");

            // Pass the request to the next component in the pipeline
            await _next(context);
        }
    }
}
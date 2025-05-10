using BookStore.Api.Extensions;
using BookStore.Api.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BookStore.Api
{
    // Startup class initializes application services and middleware
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Registers services (DI container)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Program>(); // BookValidator is in same assembly

            services.AddSwaggerGen();

            // Register custom services like FakeBookService
            services.AddCustomServices();
        }

        // Defines request pipeline middleware
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>(); // Handles unhandled exceptions globally
            app.UseMiddleware<RequestLoggingMiddleware>();     // Logs every HTTP request

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
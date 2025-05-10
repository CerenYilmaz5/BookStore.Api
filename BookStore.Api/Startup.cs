using BookStore.Api.Configurations;
using BookStore.Api.Middlewares;
using BookStore.Api.Services.Implementations;
using BookStore.Api.Services.Interfaces;
using BookStore.Api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore.Api
{
    // This class handles the configuration of services and middleware.
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Registers services and dependencies for DI
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // FluentValidation setup
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UpdateBookRequestValidator>();

            // Bind JWT settings from configuration
            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

            // JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            // Register application services
            services.AddSingleton<IBookService, FakeBookService>();
            services.AddSingleton<IJwtService, JwtService>();
        }

        // Configures the middleware pipeline
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // Middleware order matters!
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

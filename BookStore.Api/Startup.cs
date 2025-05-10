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
    // Configures services and the middleware pipeline
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Registers application services and dependencies
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // FluentValidation setup
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UpdateBookRequestValidator>();

            // JWT settings binding
            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

            // JWT Authentication configuration
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
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            // Dependency Injection
            services.AddSingleton<IBookService, FakeBookService>();
            services.AddSingleton<IAuthorService, FakeAuthorService>();
            services.AddSingleton<IJwtService, JwtService>();
        }

        // Configures the request pipeline
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // Custom middlewares
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

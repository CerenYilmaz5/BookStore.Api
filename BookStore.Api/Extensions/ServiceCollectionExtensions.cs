using BookStore.Api.Services.Implementations;
using BookStore.Api.Services.Interfaces;

namespace BookStore.Api.Extensions
{
    // This extension method adds services to the dependency injection container.
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Register the fake book service as the implementation for IBookService
            services.AddSingleton<IBookService, FakeBookService>();
            return services;
        }
    }
}
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Filters;
using Presentation.Middlewares;

namespace Presentation.DI
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddTransient<LogMiddleware>();

            return services;
        }

        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddScoped<IExceptionFilter, ExceptionsFilter>();
            services.AddScoped<IResultFilter, ResultFilter>();
            return services;
        }
    }
}

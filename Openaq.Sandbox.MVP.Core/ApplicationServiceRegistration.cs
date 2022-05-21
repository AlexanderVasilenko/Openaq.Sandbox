using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Openaq.Sandbox.MVP.Core
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

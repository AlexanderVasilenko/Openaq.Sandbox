using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Openaq.Sandbox.MVP.Core.Infrastructure;
using Openaq.Sandbox.MVP.Domain.Services.Openaq;
using Openaq.Sandbox.MVP.Infrastructure.Services;

namespace Openaq.Sandbox.MVP.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceSettings>(configuration.GetSection("ServiceSettings"));

            services.AddTransient<IOpenaqService, OpenaqService>();
            return services;
        }
    }
}

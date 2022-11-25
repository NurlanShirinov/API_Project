using AspNetCoreRateLimit;
using NetCore.AutoRegisterDi;
using System.Reflection;
using TurboAz.Core.Models;
using TurboAz.Repository.Infrustructure;
using TurboAz.Repository.Repositories.Concrete;
using TurboAz.Service.Services.Abstract;
using TurboAz.Service.Services.Concrete;

namespace TurboAz.API.Infrastructure
{
    public static class ProjectDependencies
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            var repositoryAssembly = Assembly.GetAssembly(typeof(CarRepository));

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Command"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
                .Where(c => c.Name.EndsWith("Query"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            var serviceAssembly = Assembly.GetAssembly(typeof(CarService));

            services.RegisterAssemblyPublicNonGenericClasses(serviceAssembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.AddSingleton<IUnitOfWork,UnitOfWork>();
            services.AddSingleton(typeof(IUnitOfWork1<>), typeof(UnitOfWork1<>));
            services.AddTransient<IUnitOfWorkAdoNet, UnitOfWorkAdoNet>();
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            services.AddTransient<IEmailService, EmailService>();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();

            return services;
        }
    }
}
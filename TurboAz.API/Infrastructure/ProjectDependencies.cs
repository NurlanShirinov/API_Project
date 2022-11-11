using NetCore.AutoRegisterDi;
using System.Reflection;
using TurboAz.Repository.Repositories.Concrete;
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

            services.AddSingleton(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }
    }
}
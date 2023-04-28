using System.Reflection;

namespace eCommerce.Products.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection InstallServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies
    )
    {
        IEnumerable<IServiceInstaller> serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var serviceInstaller in serviceInstallers)
        {
            serviceInstaller.Install(services, configuration);
        }

        return services;

        static bool IsAssignableType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) && !typeInfo.IsInterface && !typeInfo.IsAbstract;
    }
}

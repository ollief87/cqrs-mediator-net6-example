using System.Reflection;

namespace ProjectName.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection RegisterAllScopedOfType<T>(this IServiceCollection services)
        {
            return services.RegisterAllScopedOfType(typeof(T));
        }

        public static IServiceCollection RegisterAllScopedOfType(this IServiceCollection services, Type interfaceType)
        {
            var typesToRegister = GetTypesToRegister(interfaceType);

            foreach (var (type, interfaceTypes) in typesToRegister)
            {
                foreach (var nonGenericInterface in interfaceTypes)
                {
                    services.AddScoped(nonGenericInterface, type);
                }
            }

            return services;
        }

        private static (Type type, IEnumerable<Type> interfaceTypes)[] GetTypesToRegister(Type interfaceType)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Concat(new[] { Assembly.GetExecutingAssembly() })
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface && t.GetInterfaces().Any(i => i == interfaceType || i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                .Select(t => (type: t, interfaceTypes: t.GetInterfaces()
                    .Where(i =>
                        i == interfaceType ||
                        i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType ||
                        i.GetInterfaces().Any(x => x == interfaceType || x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType)
                    )))
                .ToArray();

            return typesToRegister;
        }
    }
}

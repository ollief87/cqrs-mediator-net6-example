using ProjectName.Api.Extensions;
using ProjectName.Infrastructure.Command;
using ProjectName.Infrastructure.Mediator;
using ProjectName.Infrastructure.Query;

namespace ProjectName.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediator, Mediator>();

            // Commands
            services.RegisterAllScopedOfType(typeof(ICommandHandler<>));
            services.RegisterAllScopedOfType(typeof(ICommandHandler<,>));

            // Queries
            services.RegisterAllScopedOfType(typeof(IQueryHandler<,>));
        }
    }
}

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Infrastructure.Command;
using ProjectName.Infrastructure.Query;

namespace ProjectName.Infrastructure.Mediator
{
    public class Mediator: IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            var handler = (dynamic) _serviceProvider.GetRequiredService(handlerType);

            return handler.Execute((dynamic)query, cancellationToken);
        }

        public async Task<TResponse> Send<TResponse>([NotNull]ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
            var handler = (dynamic) _serviceProvider.GetRequiredService(handlerType);

            var result =  await handler.Execute((dynamic)command, cancellationToken);
            return result;
        }

        public async Task Send([NotNull]ICommand command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var handler = (dynamic) _serviceProvider.GetRequiredService(handlerType);

            await handler.Execute((dynamic)command, cancellationToken);
        }
    }
}

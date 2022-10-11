using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Execute(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Execute(TCommand command, CancellationToken cancellationToken);
    }
}

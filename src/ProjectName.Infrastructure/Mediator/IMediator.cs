using System.Threading;
using System.Threading.Tasks;
using ProjectName.Infrastructure.Command;
using ProjectName.Infrastructure.Query;

namespace ProjectName.Infrastructure.Mediator
{
    public interface IMediator
    {
        public Task<TResponse> Send<TResponse>(IQuery<TResponse> request, CancellationToken cancellationToken = default);
        public Task Send(ICommand request, CancellationToken cancellationToken = default);
        public Task<TResponse> Send<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default);
    }
}

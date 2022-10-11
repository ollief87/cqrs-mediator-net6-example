using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Query
{
    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Execute(TQuery query, CancellationToken cancellationToken = default);
    }
}

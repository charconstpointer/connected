using System.Threading;
using System.Threading.Tasks;
using Connected.Api.Domain.Entities;

namespace Connected.Api.Auth
{
    public interface IUserAccessor
    {
        Task<User> GetUserFromContext(CancellationToken cancellationToken);
    }
}
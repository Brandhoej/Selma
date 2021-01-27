using System;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Infrastructure.Persistent.Abstractions
{
    public interface IContext
        : IEquatable<IContext>
    {
        int SaveChanges();
        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

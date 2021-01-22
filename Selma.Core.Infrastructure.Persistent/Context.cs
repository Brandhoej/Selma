using Selma.Core.Infrastructure.Persistent.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Infrastructure.Persistent
{
    public abstract class Context : IContext
    {
        public ValueTask<int> SaveChangesAsync()
            => SaveChangesAsync(default);

        public abstract int SaveChanges();
        public abstract ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

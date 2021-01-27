using System;
using System.Threading;
using System.Threading.Tasks;
using Selma.Core.Domain.Abstractions;

namespace Selma.Core.Infrastructure.Persistent.Abstractions
{
    public interface IUnitOfWork<TContext>
        : IEquatable<IUnitOfWork<TContext>>
        where TContext 
        : IContext
    {
        IRepository<TEntity, Guid> Repository<TEntity>()
            where TEntity 
            : class
            , IEntityRoot<Guid>;

        IRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity 
            : class
            , IEntityRoot<TId>;

        int SaveChanges();
        ValueTask<int> SaveChangesAsync();
        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

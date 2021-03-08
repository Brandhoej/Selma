using System;
using System.Threading;
using System.Threading.Tasks;
using Selma.Core.Domain.Abstractions;

namespace Selma.Core.Infrastructure.Persistent.Abstractions
{
    public interface IUnitOfWork
        : IEquatable<IUnitOfWork>
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity 
            : class
            , IEntityRoot;

        IRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity 
            : class
            , IEntityRoot<TId>;

        int SaveChanges();
        ValueTask<int> SaveChangesAsync();
        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

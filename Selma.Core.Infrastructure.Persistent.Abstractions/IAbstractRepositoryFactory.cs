using Selma.Core.Domain.Abstractions;
using System;

namespace Selma.Core.Infrastructure.Persistent.Abstractions
{
    public interface IAbstractRepositoryFactory<TContext>
       where TContext 
        : IContext
    {
        IRepository<TEntity> Repository<TEntity>(TContext context)
            where TEntity 
            : class
            , IEntityRoot<Guid>;

        IRepository<TEntity, TId> Repository<TEntity, TId>(TContext context)
            where TEntity 
            : class
            , IEntityRoot<TId>;
    }
}

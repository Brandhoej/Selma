using Selma.Core.Domain.Abstractions;
using System;

namespace Selma.Core.Infrastructure.Persistent.Abstractions
{
    public interface IAbstractRepositoryFactory<TContext>
        : IEquatable<IAbstractRepositoryFactory<TContext>>
        where TContext 
        : class
        , IContext
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity 
            : class
            , IEntityRoot;

        IRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity 
            : class
            , IEntityRoot<TId>;
    }
}

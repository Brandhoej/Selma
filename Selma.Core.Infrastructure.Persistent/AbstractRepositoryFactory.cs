using Selma.Core.Domain.Abstractions;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;

namespace Selma.Core.Infrastructure.Persistent
{
    /*public abstract class AbstractRepositoryFactory<TContext> : IAbstractRepositoryFactory<TContext>
        where TContext : IContext
    {
        public IRepository<TEntity> Repository<TEntity>(TContext context)
            where TEntity : class, IEntityRoot<Guid>
            => (this as IAbstractRepositoryFactory<TContext>).Repository<TEntity, Guid>(context) as IRepository<TEntity>;

        public abstract IRepository<TEntity, TId> Repository<TEntity, TId>(TContext context)
            where TEntity : class, IEntityRoot<TId>;
    }*/
}

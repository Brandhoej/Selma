using Selma.Core.Infrastructure.Persistent.Abstractions;
using Selma.Core.Infrastructure.Persistent.EntityFramework;
using System;

namespace Selma.IdentityProvider.Infrastructure.Persistent.EntityFramework
{
    public class AbstractRepositoryFactory : IAbstractRepositoryFactory<Context>
    {
        IRepository<TEntity> IAbstractRepositoryFactory<Context>.Repository<TEntity>(Context context)
            => (IRepository<TEntity>)((IAbstractRepositoryFactory<Context>)this).Repository<TEntity, Guid>(context);

        IRepository<TEntity, TId> IAbstractRepositoryFactory<Context>.Repository<TEntity, TId>(Context context)
            => new Repository<TEntity, TId>(context.Set<TEntity>());
    }
}

using Selma.Core.Domain.Abstractions;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;

namespace Selma.Core.Infrastructure.Persistent
{
    public abstract class AbstractRepositoryFactory<TContext>
        : IAbstractRepositoryFactory<TContext>
        where TContext
        : class
        , IContext
    {
        public AbstractRepositoryFactory(TContext context)
            => Context = context;

        protected TContext Context { get; }

        public abstract IRepository<TEntity> Repository<TEntity>()
            where TEntity
            : class
            , IEntityRoot;

        public abstract IRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity 
            : class
            , IEntityRoot<TId>;

        public override bool Equals(object obj)
            => new AbstractRepositoryFactoryEqualityComparer<TContext>().Equals(this, obj);

        public bool Equals(IAbstractRepositoryFactory<TContext> other)
            => new AbstractRepositoryFactoryEqualityComparer<TContext>().Equals(this, other);

        public override int GetHashCode()
            => new AbstractRepositoryFactoryEqualityComparer<TContext>().GetHashCode(this);

        public static bool operator !=(AbstractRepositoryFactory<TContext> left, IAbstractRepositoryFactory<TContext> right)
            => !(left == right);

        public static bool operator ==(AbstractRepositoryFactory<TContext> left, IAbstractRepositoryFactory<TContext> right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}

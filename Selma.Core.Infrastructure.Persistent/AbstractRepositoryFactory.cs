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
        public IRepository<TEntity> Repository<TEntity>(TContext context)
            where TEntity
            : class
            , IEntityRoot<Guid>
            => (this as IAbstractRepositoryFactory<TContext>).Repository<TEntity, Guid>(context) as IRepository<TEntity>;

        public abstract IRepository<TEntity, TId> Repository<TEntity, TId>(TContext context)
            where TEntity 
            : class
            , IEntityRoot<TId>;

        public override bool Equals(object obj)
            => new AbstractRepositoryFactoryEqualityComparer<TContext>().Equals(this, obj);

        public bool Equals(IAbstractRepositoryFactory<TContext> other)
            => new AbstractRepositoryFactoryEqualityComparer<TContext>().Equals(this, other);

        public override int GetHashCode()
            => base.GetHashCode();

        public override string ToString()
            => base.ToString();

        public static bool operator !=(AbstractRepositoryFactory<TContext> left, IAbstractRepositoryFactory<TContext> right)
            => !(left == right);

        public static bool operator ==(AbstractRepositoryFactory<TContext> left, IAbstractRepositoryFactory<TContext> right)
        {
            if (left is null && right is null)
            {
                return false;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}

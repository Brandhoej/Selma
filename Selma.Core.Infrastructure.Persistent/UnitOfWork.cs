using System;
using System.Threading;
using System.Threading.Tasks;
using Selma.Core.Infrastructure.Persistent.Abstractions;

namespace Selma.Core.Infrastructure.Persistent
{
    public sealed class UnitOfWork<TContext>
        : IUnitOfWork
        where TContext
        : class
        , IContext
    {
        public UnitOfWork(TContext context, IAbstractRepositoryFactory<TContext> abstractRepositoryFactory)
        {
            AbstractRepositoryFactory = abstractRepositoryFactory ?? throw new ArgumentNullException(nameof(abstractRepositoryFactory));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private TContext Context { get; }
        private IAbstractRepositoryFactory<TContext> AbstractRepositoryFactory { get; }

        public int SaveChanges()
            => Context.SaveChanges();

        public ValueTask<int> SaveChangesAsync()
            => Context.SaveChangesAsync();

        public ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken)
            => Context.SaveChangesAsync(cancellationToken);

        public override bool Equals(object obj)
            => new UnitOfWorkEqualityComparer<TContext>().Equals(this, obj);

        public bool Equals(IUnitOfWork other)
            => new UnitOfWorkEqualityComparer<TContext>().Equals(this, other);

        public override int GetHashCode()
            => new UnitOfWorkEqualityComparer<TContext>().GetHashCode(this);

        IRepository<TEntity> IUnitOfWork.Repository<TEntity>()
            => AbstractRepositoryFactory.Repository<TEntity>();

        IRepository<TEntity, TId> IUnitOfWork.Repository<TEntity, TId>()
            => AbstractRepositoryFactory.Repository<TEntity, TId>();

        public static bool operator !=(UnitOfWork<TContext> left, IUnitOfWork right)
            => !(left == right);

        public static bool operator ==(UnitOfWork<TContext> left, IUnitOfWork right)
        {
            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }
    }
}

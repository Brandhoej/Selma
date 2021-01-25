using System;
using System.Threading;
using System.Threading.Tasks;
using Selma.Core.Domain.Abstractions;
using Selma.Core.Infrastructure.Persistent.Abstractions;

namespace Selma.Core.Infrastructure.Persistent
{
    public sealed class UnitOfWork<TContext> : IUnitOfWork<TContext>
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

        public IRepository<TEntity, Guid> Repository<TEntity>()
            where TEntity : class, IEntityRoot<Guid>
            => Repository<TEntity, Guid>();

        public IRepository<TEntity, TId> Repository<TEntity, TId>()
            where TEntity : class, IEntityRoot<TId>
        {
            return AbstractRepositoryFactory.Repository<TEntity, TId>(Context);
        }

        public int SaveChanges()
            => Context.SaveChanges();
        public ValueTask<int> SaveChangesAsync()
            => Context.SaveChangesAsync();
        public ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken)
            => Context.SaveChangesAsync(cancellationToken);
    }
}

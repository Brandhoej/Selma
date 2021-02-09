using Selma.Core.Infrastructure.Persistent.Abstractions;
using Selma.Core.Infrastructure.Persistent.EntityFramework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.REST.Domain
{
    public class UnitOfWork<TContext>
        : IUnitOfWork
        where TContext
        : Context
    {
        public UnitOfWork(TContext context)
        {
            Context = context;
            RepositoryFactory = new RepositoryFactory(Context);
        }

        private TContext Context { get; }
        private IAbstractRepositoryFactory<Context> RepositoryFactory { get; }

        public bool Equals(IUnitOfWork other)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
            => Context.SaveChanges();

        public ValueTask<int> SaveChangesAsync()
            => SaveChangesAsync(default);

        public ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken)
            => new ValueTask<int>(Context.SaveChangesAsync());

        IRepository<TEntity> IUnitOfWork.Repository<TEntity>()
            => RepositoryFactory.Repository<TEntity>();

        IRepository<TEntity, TId> IUnitOfWork.Repository<TEntity, TId>()
            => RepositoryFactory.Repository<TEntity, TId>();
    }
}

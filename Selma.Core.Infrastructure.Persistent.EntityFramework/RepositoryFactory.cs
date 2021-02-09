namespace Selma.Core.Infrastructure.Persistent.EntityFramework
{
    public sealed class RepositoryFactory
        : AbstractRepositoryFactory<Context>
    {
        public RepositoryFactory(Context context)
            : base(context)
        { }

        public override Abstractions.IRepository<TEntity, TId> Repository<TEntity, TId>()
            => new Repository<TEntity, TId>(Context.Set<TEntity>());

        public override Abstractions.IRepository<TEntity> Repository<TEntity>()
            => new Repository<TEntity>(Context.Set<TEntity>());
    }
}

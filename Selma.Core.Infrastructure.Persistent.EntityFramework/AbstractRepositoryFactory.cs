namespace Selma.Core.Infrastructure.Persistent.EntityFramework
{
    public sealed class AbstractRepositoryFactory
        : AbstractRepositoryFactory<Context>
    {
        public override Abstractions.IRepository<TEntity, TId> Repository<TEntity, TId>(Context context)
            => new Repository<TEntity, TId>(context.Set<TEntity>());
    }
}

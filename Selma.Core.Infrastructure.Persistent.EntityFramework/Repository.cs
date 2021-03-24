using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Selma.Core.Domain.Abstractions;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Infrastructure.Persistent.EntityFramework
{
    public class Repository<TEntity> 
        : Repository<TEntity, Guid>
        , IRepository<TEntity>
        where TEntity 
        : class
        , IEntityRoot<Guid>
    {
        public Repository(DbSet<TEntity> rootEntities)
            : base(rootEntities)
        { }
    }

    public class Repository<TEntity, TId>
        : IRepository<TEntity, TId>
        where TEntity 
        : class
        , IEntityRoot<TId>
    {
        public Repository(DbSet<TEntity> rootEntities)
            => RootEntities = rootEntities;

        private DbSet<TEntity> RootEntities { get; }

        public TEntity Create(TEntity entity)
            => RootEntities.Add(entity).Entity;

        public ValueTask<TEntity> CreateAsync(TEntity entity)
            => CreateAsync(entity, default);

        public ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                ValueTask<EntityEntry<TEntity>> task = RootEntities.AddAsync(entity, cancellationToken);
                return new ValueTask<TEntity>(Task.FromResult(task.Result.Entity));
            }
            catch (Exception exception)
            {
                return new ValueTask<TEntity>(Task.FromException<TEntity>(exception));
            }
        }

        public void CreateRange(IEnumerable<TEntity> entities)
            => RootEntities.AddRange(entities);

        public void CreateRange(params TEntity[] entities)
            => RootEntities.AddRangeAsync(entities);

        public Task CreateRangeAsync(IEnumerable<TEntity> entities)
            => CreateRangeAsync(entities);

        public Task CreateRangeAsync(params TEntity[] entities)
            => CreateRangeAsync(entities);

        public Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
            => CreateRangeAsync(entities, cancellationToken);

        public Task CreateRangeAsync(TEntity[] entities, CancellationToken cancellationToken)
        {
            try
            {
                return RootEntities.AddRangeAsync(entities, cancellationToken);
            }
            catch (Exception exception)
            {
                return Task.FromException(exception);
            }
        }

        public TEntity Read(TId key)
            => RootEntities.Find(key);

        public IEnumerable<TEntity> ReadAll()
            => RootEntities;

        public ValueTask<TEntity> ReadAsync(TId key)
            => ReadAsync(key, default);

        public ValueTask<TEntity> ReadAsync(TId key, CancellationToken cancellationToken)
        {
            try
            {
                return RootEntities.FindAsync(key, cancellationToken);
            }
            catch (Exception exception)
            {
                return new ValueTask<TEntity>(Task.FromException<TEntity>(exception));
            }
        }

        public TEntity Update(TEntity entity)
            => RootEntities.Update(entity).Entity;

        public void UpdateRange(params TEntity[] entities)
            => UpdateRange(entities);

        public void UpdateRange(IEnumerable<TEntity> entities)
            => RootEntities.UpdateRange(entities);

        public TEntity Delete(TId key)
        {
           TEntity entityToDelete = Read(key);
            if (entityToDelete != null)
            {
                return Delete(entityToDelete);
            }
            throw new NullReferenceException($"Entity with the primary key {key} could nto be found");
        }

        public TEntity Delete(TEntity entity)
            => RootEntities.Remove(entity).Entity;

        public void DeleteRange(IEnumerable<TEntity> entities)
            => DeleteRange(entities);

        public void DeleteRange(params TEntity[] entities)
            => RootEntities.RemoveRange(entities);

        public bool Equals(IRepository<TEntity, TId> other)
            => new RepositoryEqualityComparer<TEntity, TId>().Equals(this, other);

        public override int GetHashCode()
            => new RepositoryEqualityComparer<TEntity, TId>().GetHashCode(this);
    }
}

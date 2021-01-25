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
        , IRepository<TEntity, Guid>
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
        {
            RootEntities = rootEntities;
        }

        private DbSet<TEntity> RootEntities { get; }

        public TEntity Create(TEntity entity)
        {
            return RootEntities.Add(entity).Entity;
        }

        public ValueTask<TEntity> CreateAsync(TEntity entity)
        {
            return CreateAsync(entity, default);
        }

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

        public void CreateRange( IEnumerable<TEntity> entities)
        {
            CreateRange(entities);
        }

        public void CreateRange( params TEntity[] entities)
        {
            RootEntities.AddRangeAsync(entities);
        }

        public Task CreateRangeAsync( IEnumerable<TEntity> entities)
        {
            return CreateRangeAsync(entities);
        }

        public Task CreateRangeAsync( params TEntity[] entities)
        {
            return CreateRangeAsync(entities);
        }

        public Task CreateRangeAsync( IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            return CreateRangeAsync(entities, cancellationToken);
        }

        public Task CreateRangeAsync( TEntity[] entities, CancellationToken cancellationToken)
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

        public TEntity Read( TId key)
        {
            return RootEntities.Find(key);
        }

        public ValueTask<TEntity> ReadAsync( TId key)
        {
            return ReadAsync(key, default);
        }

        public ValueTask<TEntity> ReadAsync( TId key, CancellationToken cancellationToken)
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
        {
            return RootEntities.Update(entity).Entity;
        }

        public void UpdateRange( params TEntity[] entities)
        {
            UpdateRange(entities);
        }

        public void UpdateRange( IEnumerable<TEntity> entities)
        {
            RootEntities.UpdateRange(entities);
        }

        public TEntity Delete( TId key)
        {
           TEntity entityToDelete = Read(key);
            if (entityToDelete != null)
            {
                return Delete(entityToDelete);
            }
            throw new NullReferenceException($"Entity with the primary key {key} could nto be found");
        }

        public TEntity Delete(TEntity entity)
        {
            return RootEntities.Remove(entity).Entity;
        }

        public void DeleteRange( IEnumerable<TEntity> entities)
        {
            DeleteRange(entities);
        }

        public void DeleteRange( params TEntity[] entities)
        {
            RootEntities.RemoveRange(entities);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Selma.Core.Domain.Abstractions;

namespace Selma.Core.Infrastructure.Persistent.Abstractions
{
    public interface IRepository<TEntity>
        : IRepository<TEntity, Guid>
        where TEntity 
        : IEntityRoot<Guid>
    { }

    public interface IRepository<TEntity, TId>
        where TEntity 
        : IEntityRoot<TId>
    {
        TEntity Create( TEntity entity);
        ValueTask<TEntity> CreateAsync( TEntity entity);
        ValueTask<TEntity> CreateAsync( TEntity entity, CancellationToken cancellationToken);
        void CreateRange( IEnumerable<TEntity> entities);
        void CreateRange( params TEntity[] entities);
        Task CreateRangeAsync( IEnumerable<TEntity> entities);
        Task CreateRangeAsync( params TEntity[] entities);
        Task CreateRangeAsync( IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task CreateRangeAsync( TEntity[] entities, CancellationToken cancellationToken);

        TEntity Read( TId key);
        ValueTask<TEntity> ReadAsync( TId key);
        ValueTask<TEntity> ReadAsync( TId key, CancellationToken cancellationToken);
        
        TEntity Update( TEntity entity);
        void UpdateRange( params TEntity[] entities);
        void UpdateRange( IEnumerable<TEntity> entities);

        TEntity Delete( TId key);
        TEntity Delete( TEntity entity);
        void DeleteRange( params TEntity[] entities);
        void DeleteRange( IEnumerable<TEntity> entities);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using Selma.Core.MessageQueue.Abstractions;
using Selma.Core.Infrastructure.Events.Abstractions;

namespace Selma.Core.Infrastructure.Persistent.EntityFramework
{
    public abstract class Context 
        : DbContext
        , IContext
        , IDisposable
    {
        public Context(DbContextOptions dbContextOptions)
            : this(default, dbContextOptions)
        { }

        public Context(IDeferredMessageQueue<IIntegrationEvent> deferredMessageQueue, DbContextOptions dbContextOptions)
            : base(dbContextOptions)
            => DeferredMessageQueue = deferredMessageQueue;

        protected IDeferredMessageQueue<IIntegrationEvent> DeferredMessageQueue { get; }

        async ValueTask<int> IContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await DeferredMessageQueue?.Dispatch();
                return await SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                return await new ValueTask<int>(Task.FromException<int>(exception));
            }
        }

        public override bool Equals(object obj)
            => new ContextEqualityComparer().Equals(this, obj);

        public bool Equals(IContext other)
            => new ContextEqualityComparer().Equals(this, other);

        public override int GetHashCode()
            => base.GetHashCode();

        public override string ToString()
            => base.ToString();
    }
}

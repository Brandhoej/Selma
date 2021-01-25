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
        public Context()
            : this(default)
        { }

        public Context(IDeferredMessageQueue<IIntegrationEvent> deferredMessageQueue)
        {
            DeferredMessageQueue = deferredMessageQueue;
        }

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
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using Selma.Core.Domain.Events.Abstractions;

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

        public Context(IDeferredDomainEventDispatcher deferredDomainEventHandler)
        {
            DeferredDomainEventHandler = deferredDomainEventHandler;
        }

        protected IDeferredDomainEventDispatcher DeferredDomainEventHandler { get; }

        async ValueTask<int> IContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await DeferredDomainEventHandler?.DispatchAll();
                return await SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                return await new ValueTask<int>(Task.FromException<int>(exception));
            }
        }
    }
}

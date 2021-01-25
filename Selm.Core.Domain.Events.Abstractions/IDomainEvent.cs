using Selma.Core.Domain.Abstractions;
using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.Domain.Events.Abstractions
{
    /// <summary>
    ///     Repåresents a domain which are mediated in a specific domain or single system
    /// </summary>
    public interface IDomainEvent
        : IDomainObject
        , IMessage
    {
        /// <summary>
        ///     Used to enqueue the <see cref="IDomainEvent"/> making
        ///     it ready for dispatching.
        /// </summary>
        void Enqueue();
    }
}

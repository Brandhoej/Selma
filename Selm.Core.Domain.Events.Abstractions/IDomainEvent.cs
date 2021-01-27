using Selma.Core.Domain.Abstractions;
using Selma.Core.MessageQueue.Abstractions;
using System;

namespace Selma.Core.Domain.Events.Abstractions
{
    /// <summary>
    ///     Represents a domain which are mediated in a specific domain or single system
    /// </summary>
    public interface IDomainEvent
        : IDomainObject
        , IEquatable<IDomainEvent>
        , IMessage
    {
        /// <summary>
        ///     Used to enqueue the <see cref="IDomainEvent"/> making
        ///     it ready for dispatching.
        /// </summary>
        void Enqueue();
    }
}

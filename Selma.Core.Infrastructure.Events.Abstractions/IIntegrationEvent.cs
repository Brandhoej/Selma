using Selma.Core.MessageQueue.Abstractions;
using System;

namespace Selma.Core.Infrastructure.Events.Abstractions
{
    public interface IIntegrationEvent
        : IMessage
        , IEquatable<IIntegrationEvent>
    { }
}

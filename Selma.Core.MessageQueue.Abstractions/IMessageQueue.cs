using System;

namespace Selma.Core.MessageQueue.Abstractions
{
    public interface IMessageQueue<TMessage>
        : IEquatable<IMessageQueue<TMessage>>
        where TMessage
        : IMessage
    { }
}

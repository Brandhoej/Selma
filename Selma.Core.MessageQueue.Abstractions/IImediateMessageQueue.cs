using System;

namespace Selma.Core.MessageQueue.Abstractions
{
    public interface IImediateMessageQueue<TMessage>
        : IMessageQueue<TMessage>
        , IMessageQueueProducer<TMessage>
        , IEquatable<IImediateMessageQueue<TMessage>>
        where TMessage
        : class
        , IMessage
    { }
}

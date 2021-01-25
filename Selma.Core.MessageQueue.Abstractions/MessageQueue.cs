using System;

namespace Selma.Core.MessageQueue.Abstractions
{
    public abstract class MessageQueue<TMessage>
        : IMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public MessageQueue(IDispatcher<TMessage> dispatcher)
            => Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

        protected IDispatcher<TMessage> Dispatcher { get; }
    }
}

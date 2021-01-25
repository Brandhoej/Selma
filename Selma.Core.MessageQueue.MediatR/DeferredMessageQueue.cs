using MediatR;
using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue.MediatR
{
    public class DeferredMessageQueue<TMessage>
        : MessageQueue.DeferredMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public DeferredMessageQueue(IPublisher publisher)
            : this(new Dispatcher<TMessage>(publisher))
        { }

        public DeferredMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }
    }
}

using MediatR;
using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue.MediatR
{
    public class ImmediateMessageQueue<TMessage>
        : MessageQueue.ImmediateMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public ImmediateMessageQueue(IPublisher publisher)
            : this(new Dispatcher<TMessage>(publisher))
        { }

        public ImmediateMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }
    }
}

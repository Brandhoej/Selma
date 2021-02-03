using MediatR;
using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue.MediatR
{
    public sealed class DeferredMessageQueue<TMessage>
        : MessageQueue.DeferredMessageQueue<TMessage>
        , IDeferredMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public DeferredMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }
    }
}

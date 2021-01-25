using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue.Kafka
{
    public class DeferredMessageQueue<TMessage>
        : MessageQueue.DeferredMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public DeferredMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        {
        }
    }
}

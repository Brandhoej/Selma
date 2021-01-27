using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue.Kafka
{
    public class ImmediateMessageQueue<TMessage>
        : MessageQueue.ImmediateMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        protected ImmediateMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }
    }
}

using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue
{
    public class ImmediateMessageQueue<TMessage>
    : MessageQueue<TMessage>
    , IImediateMessageQueue<TMessage>
    where TMessage
    : class
    , IMessage
    {
        protected ImmediateMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }

        public void Enqueue(TMessage element)
            => Dispatcher.Dispatch(element);
    }
}

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

        public bool Equals(IImediateMessageQueue<TMessage> other)
        {
            throw new System.NotImplementedException();
        }

        public override bool Equals(object obj)
            => base.Equals(obj);

        public override int GetHashCode()
            => base.GetHashCode();

        public override string ToString()
            => base.ToString();
    }
}

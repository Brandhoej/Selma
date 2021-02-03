using Selma.Core.MessageQueue.Abstractions;
using System;

namespace Selma.Core.MessageQueue
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

        public bool Equals(IMessageQueue<TMessage> other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

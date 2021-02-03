using MediatR;
using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue.MediatR
{
    public sealed class ImmediateMessageQueue<TMessage>
        : MessageQueue.ImmediateMessageQueue<TMessage>
        , IImediateMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public ImmediateMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }
    }
}

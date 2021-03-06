﻿using Selma.Core.MessageQueue.Abstractions;

namespace Selma.Core.MessageQueue.Kafka
{
    public sealed class ImmediateMessageQueue<TMessage>
        : MessageQueue.ImmediateMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public ImmediateMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }
    }
}

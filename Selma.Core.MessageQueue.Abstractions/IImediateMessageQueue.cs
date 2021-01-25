namespace Selma.Core.MessageQueue.Abstractions
{
    public interface IImediateMessageQueue<TMessage>
        : IMessageQueue<TMessage>
        , IMessageQueueProducer<TMessage>
        where TMessage
        : class
        , IMessage
    { }
}

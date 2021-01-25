namespace Selma.Core.MessageQueue.Abstractions
{
    public interface IMessageQueueProducer<TMessage>
        where TMessage
        : IMessage
    {
        void Enqueue(TMessage element);
    }
}

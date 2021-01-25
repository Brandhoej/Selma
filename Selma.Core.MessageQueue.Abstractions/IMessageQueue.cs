namespace Selma.Core.MessageQueue.Abstractions
{
    public interface IMessageQueue<TMessage>
        where TMessage
        : IMessage
    { }
}

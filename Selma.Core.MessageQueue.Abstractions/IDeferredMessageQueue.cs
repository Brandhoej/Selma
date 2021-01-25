using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Selma.Core.MessageQueue.Abstractions
{
    public interface IDeferredMessageQueue<TMessage>
        : IMessageQueue<TMessage>
        , IMessageQueueProducer<TMessage>
        , IEnumerable<TMessage>
        , IEnumerable
        , IReadOnlyCollection<TMessage>
        , ICollection
        where TMessage
        : class
        , IMessage
    {
        Task Dispatch();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Selma.Core.MessageQueue.Abstractions
{
    public interface IDispatcher<TMessage>
        where TMessage
        : IMessage
    {
        Task Dispatch(TMessage message);
        Task Dispatch(IEnumerable<TMessage> messages);
    }
}

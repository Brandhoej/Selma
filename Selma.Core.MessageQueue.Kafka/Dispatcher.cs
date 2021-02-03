using System.Collections.Generic;
using Selma.Core.MessageQueue.Abstractions;
using System.Threading.Tasks;
using System.Linq;

namespace Selma.Core.MessageQueue.Kafka
{
    public sealed class Dispatcher<TMessage>
        : IDispatcher<TMessage>
        where TMessage
        : IMessage
    {
        public Dispatcher()
        { }

        public Task Dispatch(TMessage message)
            => Task.Factory.StartNew(() => System.Diagnostics.Debug.WriteLine("Kafka dispatcher not implemented"));

        public Task Dispatch(IEnumerable<TMessage> messages)
            => Task.WhenAll(messages.Select(
                message => Dispatch(message)));
    }
}

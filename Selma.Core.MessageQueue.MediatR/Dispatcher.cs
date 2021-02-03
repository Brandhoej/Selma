using MediatR;
using System.Collections.Generic;
using Selma.Core.MessageQueue.Abstractions;
using System.Threading.Tasks;
using System.Linq;

namespace Selma.Core.MessageQueue.MediatR
{
    public sealed class Dispatcher<TMessage>
        : IDispatcher<TMessage>
        where TMessage
        : IMessage
    {
        public Dispatcher(IPublisher publisher)
            => Publisher = publisher;

        private IPublisher Publisher { get; }

        public Task Dispatch(TMessage message)
            => Publisher.Publish(message);

        public Task Dispatch(IEnumerable<TMessage> messages)
            => Task.WhenAll(messages.Select(
                message => Dispatch(message)));
    }
}

using Selma.Core.MessageQueue.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Selma.Core.MessageQueue
{
    public class DeferredMessageQueue<TMessage>
        : MessageQueue<TMessage>
        , IDeferredMessageQueue<TMessage>
        where TMessage
        : class
        , IMessage
    {
        public DeferredMessageQueue(IDispatcher<TMessage> dispatcher)
            : base(dispatcher)
        { }

        public int Count => Queue.Count;

        public bool IsSynchronized => (Queue as ICollection).IsSynchronized;

        public object SyncRoot => (Queue as ICollection).SyncRoot;

        private Queue<TMessage> Queue { get; } = new Queue<TMessage>();

        public void CopyTo(Array array, int index)
            => Queue.CopyTo(array as TMessage[], index);

        public Task Dispatch()
            => Dispatcher.Dispatch(Queue);

        public void Enqueue(TMessage element)
            => Queue.Enqueue(element);

        public IEnumerator<TMessage> GetEnumerator()
            => Queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}

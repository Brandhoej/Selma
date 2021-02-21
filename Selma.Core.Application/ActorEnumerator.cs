using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Application
{
    internal class ActorEnumerator
        : IEnumerator<IActor>
        , IEnumerator
    {
        internal ActorEnumerator(IActor actor)
            => CurrentChild = Root = actor;

        private IActor Root { get; }
        private IActor CurrentChild { get; set; }

        public object Current => CurrentChild;
        IActor IEnumerator<IActor>.Current => CurrentChild;

        public bool MoveNext()
            => (CurrentChild = CurrentChild.Successor) != null;

        public void Reset()
            => CurrentChild = Root;

        public void Dispose() { }
    }
}

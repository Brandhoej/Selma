using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Application
{
    internal class ActorEnumerable
        : IEnumerable<IActor>
        , IEnumerable
    {
        internal ActorEnumerable(IActor actor)
            => Actor = actor;

        private IActor Actor { get; }

        public IEnumerator GetEnumerator()
            => GetEnumerator();

        IEnumerator<IActor> IEnumerable<IActor>.GetEnumerator()
            => new ActorEnumerator(Actor);
    }
}

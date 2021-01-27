using MediatR;
using Selma.Core.Domain.Events.Abstractions;
using System.Collections;
using System.Collections.Generic;

namespace Selma.Core.Domain.Events
{
    internal class DomainEventHandlerEqualityComparer<T>
        : IEqualityComparer<IDomainEventHandler<T>>
        , IEqualityComparer
        where T
        : class
        , IDomainEvent
        , INotification
    {
        public new bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (!x.GetType().Equals(y.GetType()))
            {
                return false;
            }

            return Equals(x as IDomainEventHandler<T>, y as IDomainEventHandler<T>);
        }

        public bool Equals(IDomainEventHandler<T> x, IDomainEventHandler<T> y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (!ReferenceEquals(x, y))
            {
                return false;
            }

            if (GetHashCode(x) == GetHashCode(y))
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(object obj)
            => GetHashCode(obj as IDomainEventHandler<T>);

        public int GetHashCode(IDomainEventHandler<T> obj)
            => obj.GetHashCode();
    }
}

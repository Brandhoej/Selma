using System;

namespace Selma.Core.Domain.Abstractions
{
    /// <summary>
    ///     Represents an immutable value object from domain driven design:
    ///     Value Objects in domain models – immutable types that are used as properties of entities.
    ///     Its only use is as a property of one or more entities or even another value object.
    ///     
    ///     The keys to designing a value object are:
    ///     1. It is immutable.
    ///     2. Its only use is as a property of one or more entities or even another value object
    ///     3. It knows to use all of its properties when performing any type of equality check,
    ///         including those which base their comparison on hash codes.
    ///     4.Its logic should have no side effects on state outside of the value object.
    /// </summary>
    public interface IValueObject
        : IDomainObject
        , IEquatable<IValueObject>
    { }
}

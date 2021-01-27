using Selma.Core.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selma.Core.Domain
{
    /// <summary>
    ///     <inheritdoc cref="IValueObject"/>
    ///     
    ///     C#9 TODO: Make this a record to enforce immutability of reference types.
    /// </summary>
    public abstract class ValueObject 
        : IValueObject
        , IEquatable<ValueObject>
    {
        /// <summary>
        ///     Returns a value indicating whether this <see cref="ValueObject"/> is equal to a specified object.
        /// </summary>
        /// <param name="obj">
        ///     An object to compare with this instance of <see cref="ValueObject"/>.
        /// </param>
        /// <returns>
        ///     true if obj is an instance of <see cref="ValueObject"/> and equals the value of this instance;
        ///     otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
            => new ValueObjectEqualityComparer().Equals(this, obj);

        /// <summary>
        ///     Returns a value indicating whether this <see cref="ValueObject"/> is equal to a specified <see cref="ValueObject"/>.
        /// </summary>
        /// <param name="other">
        ///     A <see cref="ValueObject"/> to compare with this instance of <see cref="ValueObject"/>.
        /// </param>
        /// <returns>
        ///     true if all components from this <see cref="GetEqualityComponents"/> is sequentially equal
        ///     to that of the other;
        ///     otherwise, false.
        /// </returns>
        public bool Equals(ValueObject other)
            => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

        /// <summary>
        ///     Returns the hash code for this instance.
        ///     The hash code is calculated through aggregating the hashes
        ///     of the different components returned from <see cref="GetEqualityComponents"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="int"/> hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(new HashCode(), (hashCode, curr) =>
                {
                    hashCode.Add(curr);
                    return hashCode;
                }).ToHashCode();
        }

        /// <summary>
        ///     This yields from this method must be that of the members which determine the value of this object.
        ///     This must be implemented since value objects equality are not based on identity.
        /// </summary>
        /// <example>
        /// <code>
        ///     public class Address : ValueObject
        ///     {
        ///         public string Street  { get; private set; }
        ///         public string City    { get; private set; }
        ///         public string State   { get; private set; }
        ///         public string Country { get; private set; }
        ///         public string ZipCode { get; private set; }
        ///     
        ///         public Address() { }
        ///     
        ///         public Address(string street, string city, string state, string country, string zipcode)
        ///         {
        ///             Street = street;
        ///             City = city;
        ///             State = state;
        ///             Country = country;
        ///             ZipCode = zipcode;
        ///         }
        ///     
        ///         protected override IEnumerable<object> GetEqualityComponents()
        ///         {
        ///             /// Using a yield return statement to return each element one at a time
        ///             yield return Street;
        ///             yield return City;
        ///             yield return State;
        ///             yield return Country;
        ///             yield return ZipCode;
        ///         }
        ///     }
        /// </code>
        /// </example>
        /// <returns>
        ///     Yields all components used to determine the identity of the instance.
        /// </returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        ///     Inequality operator implementation of the <see cref="ValueObject"/>.
        /// </summary>
        /// <param name="left">
        ///     Left side of the inequality operator
        /// </param>
        /// <param name="right">
        ///     Right side of the inequality operator
        /// </param>
        /// <returns>
        ///     False if <paramref name="left"/> is equal to <paramref name="right"/>;
        ///     otherwise, true.
        /// </returns>
        public static bool operator !=(ValueObject left, ValueObject right)
            => !(left == right);

        /// <summary>
        ///     Equality operator implementation of the <see cref="ValueObject"/>.
        /// </summary>
        /// <param name="left">
        ///     Left side of the equality operator
        /// </param>
        /// <param name="right">
        ///     Right side of the equality operator
        /// </param>
        /// <returns>
        ///     True if <paramref name="left"/> is equal to <paramref name="right"/>;
        ///     otherwise, false.
        /// </returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;
            
            return left.Equals(right);
        }
    }
}

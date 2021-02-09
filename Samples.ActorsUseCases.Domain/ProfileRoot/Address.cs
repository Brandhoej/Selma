using Selma.Core.Domain;
using System.Collections.Generic;

namespace Samples.ActorsUseCases.Domain.ProfileRoot
{
    public sealed class Address
        : ValueObject
    {
        public Address(string road, int number)
        {
            Road = road;
            Number = number;
        }

        private Address()
        { }

        public string Road { get; private set; }
        public int Number { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Road;
            yield return Number;
        }

        public override string ToString()
            => $"{Road} {Number}";
    }
}

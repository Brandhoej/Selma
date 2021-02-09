using Selma.Core.Domain;
using Selma.Core.Domain.Abstractions;
using System;

namespace Samples.ActorsUseCases.Domain.ProfileRoot
{
    public interface IProfile
        : IEntityRoot
    {
        string Name { get; }
        string Email { get; }
        bool Activated { get; }
        DateTime ActivationDateTime { get; }
        Address Address { get; }

        void Activate();
    }

    internal class Profile 
        : EntityRoot
        , IProfile
    {
        internal Profile(string name, string email, Address address)
            : base()
        {
            Name = name;
            Email = email;
            Address = address;
        }

        private Profile()
        { }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool Activated { get; private set; }
        public DateTime ActivationDateTime { get; private set; }
        public Address Address { get; private set; }

        public void Activate()
        {
            Activated = true;
            ActivationDateTime = DateTime.Now;
            new ProfileActivatedDomainEvent(this).Enqueue();
        }
    }
}

using Selma.Core.Domain;
using Selma.Core.Domain.Abstractions;
using Selma.Core.FSM;
using Selma.Core.FSM.Abstractions;
using System;
using System.Collections.Generic;

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
        private enum State
        {
            ProfileNotActivated,
            ProfileActivated
        };

        private enum Event
        {
            ProfileActivated
        };

        internal Profile(string name, string email, Address address)
            : this()
        {
            Name = name;
            Email = email;
            Address = address;
        }

        private Profile()
            : base()
        {
            IEntityFSMBuilder builder = new EntityFSMBuilder(2, 1);
            builder.AddTransition(State.ProfileNotActivated, Event.ProfileActivated, State.ProfileActivated);
            Fsm = builder.Build();
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool Activated { get; private set; }
        public DateTime ActivationDateTime { get; private set; }
        public Address Address { get; private set; }

        private IFSM<int, int> Fsm { get; }

        public void Activate()
        {
            if (Fsm.TryTransition((int)Event.ProfileActivated, out int _))
            {
                Activated = true;
                ActivationDateTime = DateTime.Now;
                new ProfileActivatedDomainEvent(this).Enqueue();
            }
            else
            {
                throw new InvalidOperationException($"The entity is in the {(State)Fsm.CurrentState} and the transition is invalid");
            }
        }
    }
}

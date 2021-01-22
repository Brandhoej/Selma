using Selma.Core.Domain;
using System;

namespace Samples.ActorsUseCases.Domain.ProfileRoot
{
    public class Profile : EntityRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool Activated { get; private set; }

        private Profile()
        { }

        public Profile(string name, string email)
            : base()
        {
            Name = name;
            Email = email;
        }

        public void Activate()
        {
            Activated = true;
            new ProfileActivatedDomainEvent
            {
                ProfileId = Id,
                ActivationDateTime = DateTime.Now
            }.Enqueue();
        }
    }
}

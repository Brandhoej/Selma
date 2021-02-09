using Selma.Core.Domain.Events;
using System;

namespace Samples.ActorsUseCases.Domain.ProfileRoot
{
    public class ProfileActivatedDomainEvent
        : DomainEvent
    {
        public ProfileActivatedDomainEvent(IProfile profile)
        {
            ProfileId = profile.Id;
            ActivationDateTime = profile.ActivationDateTime;
        }

        public Guid ProfileId { get; set; }
        public DateTime ActivationDateTime { get; set; }
    }
}

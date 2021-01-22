using Selma.Core.Domain;
using Selma.Core.Domain.Events;
using System;

namespace Samples.ActorsUseCases.Domain.ProfileRoot
{
    public class ProfileActivatedDomainEvent : DomainEvent
    {
        public Guid ProfileId { get; set; }
        public DateTime ActivationDateTime { get; set; }
    }
}

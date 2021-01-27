using System;
using System.Threading;
using System.Threading.Tasks;
using Samples.ActorsUseCases.Domain.ProfileRoot;
using Selma.Core.Domain.Events;

namespace Samples.ActorsUseCases.Application.Notification
{
    public class ProfileActivatedNotificationHandler
        : DomainEventHandler<ProfileActivatedDomainEvent>
    {
        public override Task Handle(ProfileActivatedDomainEvent notification, CancellationToken cancellationToken = default)
            => Task.Run(() => Console.WriteLine($"{notification.ProfileId} was activated at {notification.ActivationDateTime}"));
    }
}

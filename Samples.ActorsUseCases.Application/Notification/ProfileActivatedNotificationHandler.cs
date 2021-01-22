using System;
using System.Threading;
using System.Threading.Tasks;
using Samples.ActorsUseCases.Domain.ProfileRoot;
using MediatR;

namespace Samples.ActorsUseCases.Application.Notification
{
    public class ProfileActivatedNotificationHandler : INotificationHandler<ProfileActivatedDomainEvent>
    {
        public Task Handle(ProfileActivatedDomainEvent notification, CancellationToken cancellationToken)
            => Task.Run(() => Console.WriteLine($"{notification.ProfileId} was activated at {notification.ActivationDateTime}"));
    }
}

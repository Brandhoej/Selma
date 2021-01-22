using MediatR;
using Selma.Core.Application;
using Selma.Core.Application.Abstractions;
using System.Collections.Generic;
using System.Reflection;

namespace Selma.IdentityProvider.Application.Actors.UnregisteredUser
{
    public class UnregisteredUserActor : Actor
    {
        //protected override IEnumerable<Assembly> GetUseCasesFrom()
        //{
        //    yield return GetType().Assembly;
        //}

        //protected override IEnumerable<Assembly> GetNotificationHandlersFrom()
        //{
        //    yield return GetType().Assembly;
        //}
        public UnregisteredUserActor(IActor successor, IMediator mediator)
            : base(successor, mediator)
        {
        }
    }
}

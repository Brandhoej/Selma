using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Selma.Core.Application.Actors.UnregisteredUser
{
    public class UnregisteredUser : Actor
    {
        protected override IEnumerable<Assembly> GetUseCasesFrom()
        {
            yield return GetType().Assembly;
        }

        protected override IEnumerable<Assembly> GetNotificationHandlersFrom()
        {
            yield return GetType().Assembly;
        }
    }
}

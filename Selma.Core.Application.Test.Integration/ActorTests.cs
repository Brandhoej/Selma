using MediatR;
using Selma.Core.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selma.Core.Application.Test.Integration
{
    public class ActorA
        : Actor
    {
        public ActorA(IMediator mediator)
            : base(mediator)
        { }

        public ActorA(IActor successor, IMediator mediator)
            : base(successor, mediator)

        { }
    }

    public class ActorB
        : Actor
    {
        public ActorB(IMediator mediator)
            : base(mediator)
        { }

        public ActorB(IActor successor, IMediator mediator)
            : base(successor, mediator)

        { }
    }

    public class ActorTests
    {
    }
}

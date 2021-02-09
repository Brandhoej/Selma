using MediatR;
using Samples.REST.Application.UseCases;
using Selma.Core.Application;
using Selma.Core.Application.Abstractions;
using System;
using System.Collections.Generic;

namespace Samples.REST.Application
{
    public interface IUser
        : IActor
    { }

    internal class User
        : Actor
        , IUser
    {
        public User(IMediator mediator)
            : base(mediator)
        { }

        public override IEnumerable<Type> GetSupportedUseCases()
        {
            yield return typeof(GetWeatherForecasts);
        }
    }
}

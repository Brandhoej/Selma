using System.Reflection;
using System.Collections.Generic;
using Selma.Core.Application;
using MediatR;
using System;
using Samples.ActorsUseCases.Application.UseCases;

namespace Samples.ActorsUseCases.Application
{
    public class User
        : Actor
    {
        public User(IMediator mediator)
            : base(default, mediator)
        { }

        public override IEnumerable<Type> GetSupportedUseCases()
        {
            yield return typeof(ActivateProfileUseCase);
            yield return typeof(GetProfileInformationUseCase);
            yield return typeof(RegisterProfileUseCase);
        }
    }
}

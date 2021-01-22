using MediatR;
using Samples.ActorsUseCases.Application.UseCases;
using Selma.Core.Application;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Samples.ActorsUseCases.Application
{
    public class User : Actor
    {
        public User(IMediator mediator)
            : base(default, mediator)
        { }

        public override IEnumerable<Assembly> GetSupportedAssemblies()
        {
            yield return GetType().Assembly;
        }

        //protected override IEnumerable<Type> GetSupportedUseCases()
        //{
        //    yield return typeof(ActivateProfileUseCase);
        //    yield return typeof(GetProfileInformationUseCase);
        //    yield return typeof(RegisterProfileUseCase);
        //}
    }
}

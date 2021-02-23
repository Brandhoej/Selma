using Selma.Core.Application.Abstractions;
using NSubstitute;
using Xunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace Selma.Core.Application.Test.Unit
{
    public class ServiceCollectionExtensionsTests
    {
        public interface IActorA
            : IActor
        { }
        public abstract class ActorA
            : Actor
            , IActorA
        {
            protected ActorA(IMediator mediator)
                : base(mediator)
            { }

            protected ActorA(IActor successor, IMediator mediator)
                : base(successor, mediator)
            { }
        }

        public class TheAddActorMethod
        {
            [Fact]
            public void AddActor_AddsAScopedServiceOfTheActorImplementationType_IfOnlyATypeIsGiven()
            {
                // Arrange
                IServiceCollection serviceCollection = new ServiceCollection();
                bool actual;

                // Act
                serviceCollection.AddActor<ActorA>();
                actual = serviceCollection.Any(service
                    => service.ImplementationType == typeof(ActorA) && 
                        service.Lifetime == ServiceLifetime.Scoped);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void AddActor_AddsAScopedServiceOfTheActorImplementationAndServiceType_IfBothTheImplementationAndServiceTypeIsGiven()
            {
                // Arrange
                IServiceCollection serviceCollection = new ServiceCollection();
                bool actual;

                // Act
                serviceCollection.AddActor<IActorA, ActorA>();
                actual = serviceCollection.Any(service
                    => service.ImplementationType == typeof(ActorA) && 
                        service.ServiceType == typeof(IActorA) &&
                        service.Lifetime == ServiceLifetime.Scoped);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void AddActor_AddsAScopedServiceOfTheActorImplementationTypeAndItsFactory_IfOnlyATypeIsGiven()
            {
                // Arrange
                IServiceCollection serviceCollection = new ServiceCollection();
                Func<IServiceProvider, ActorA> factory = provider => default;
                bool actual;

                // Act
                serviceCollection.AddActor(factory);
                actual = serviceCollection.Any(service
                    => service.ImplementationFactory == factory &&
                        service.Lifetime == ServiceLifetime.Scoped);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void AddActor_Throws_IfOnlyATypeIsGivenAndTheImplementationFactoryIsNull()
            {
                // Arrange
                IServiceCollection serviceCollection = new ServiceCollection();
                Func<IServiceProvider, ActorA> factory = default;

                // Act
                void Test()
                {
                    serviceCollection.AddActor(factory);
                }

                // Assert
                Assert.Throws<ArgumentNullException>(Test);
            }

            [Fact]
            public void AddActor_AddsAScopedServiceOfTheActorImplementationAndServiceTypeAndItsFactory_IfBothTheImplementationAndServiceTypeIsGiven()
            {
                // Arrange
                IServiceCollection serviceCollection = new ServiceCollection();
                Func<IServiceProvider, ActorA> factory = provider => default;
                bool actual;

                // Act
                serviceCollection.AddActor<IActorA, ActorA>(factory);
                actual = serviceCollection.Any(service
                    => service.ServiceType == typeof(IActorA) &&
                        service.ImplementationFactory == factory &&
                        service.Lifetime == ServiceLifetime.Scoped);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void AddActor_Throws_IfOnlyAnImplementationTypeAndServiceTypeIsGivenAndTheImplementationFactoryIsNull()
            {
                // Arrange
                IServiceCollection serviceCollection = new ServiceCollection();
                Func<IServiceProvider, ActorA> factory = default;

                // Act
                void Test()
                {
                    serviceCollection.AddActor<IActorA, ActorA>(factory);
                }

                // Assert
                Assert.Throws<ArgumentNullException>(Test);
            }
        }
    }
}

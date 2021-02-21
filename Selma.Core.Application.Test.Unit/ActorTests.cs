using MediatR;
using NSubstitute;
using Selma.Core.Application.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Selma.Core.Application.Test.Unit
{
    public class ActorTests
    {
        public class TheConstructorMethod
        {
            [Fact]
            public void Constructor_SetsTheSuccessor_IfASuccessorWasGivenAsParameter()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IActor expected = Substitute.For<IActor, Actor>(mediator);
                IActor actor;

                // Act
                actor = Substitute.ForPartsOf<Actor>(expected, mediator);

                // Assert
                Assert.NotNull(actor.Successor);
            }

            [Fact]
            public void Constructor_DoesNotSetTheSuccessor_WhenASuccessorWasNotGivenAsParameter()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IActor actor;

                // Act
                actor = Substitute.ForPartsOf<Actor>(mediator);

                // Assert
                Assert.Null(actor.Successor);
            }
        }

        public class TheCountProperty
        {
            [Fact]
            public void CountGet_IsOne_IfNoSuccessor()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IActor actor = Substitute.ForPartsOf<Actor>(mediator);
                int expected = 1, actual;

                // Act
                actual = actor.Count;

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CountGet_IsTheCountOfTheSuccessorPlus1_IfASuccessorIsGiven()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IActor successor = Substitute.For<IActor, Actor>(mediator);
                IActor actor = Substitute.ForPartsOf<Actor>(successor, mediator);
                int expected = 2, actual;

                successor.Count.ReturnsForAnyArgs(0);
                successor.Successor.ReturnsForAnyArgs(_ => default);

                // Act
                actual = actor.Count;

                // Assert
                Assert.Equal(expected, actual);
            }
        }

        public class TheDoMethod
        {
            [Fact]
            public void SupportsUseCase_ReturnsFalse_IfNoSupportedUseCaseTypesAndAssembliesHasBeenDefined()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IActor actor = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actor.SupportsUseCaseWithResponse<object>();

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Do_ReturnsAValueTask_IfTheUseCaseIsSupportedByTheActorOrSuccesor()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IActor successor = Substitute.For<IActor, Actor>(mediator);
                IActor actor = Substitute.ForPartsOf<Actor>(successor, mediator);
                object actual;

                successor.SupportsUseCaseWithResponse<object>().Returns(true);
                mediator.Send(Arg.Any<object>(), Arg.Any<CancellationToken>()).Returns(new { });

                // Act
                actual = actor.Do(Arg.Any<IUseCaseRequest<object>>());

                // Assert
                Assert.NotNull(actual);
            }
        }

        public class TheSupportsUseCaseWithResponseMethod
        {
            [Fact]
            public void SupportsUseCaseWithResponse_ReturnsFalse_WhenNoUseCaseResponseWasFound()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IActor actor = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actor.SupportsUseCaseWithResponse<object>();

                // Assert
                Assert.False(actual);
            }
        }

        public class TheEqualsMethod
        {
            /*
             * - bool Equals(object x, object y)
             * object x is null
             * object y is null
             * Different object types -> return false
             * 
             * - bool Equals(IActor x, IActor y)
             * actor x is null
             * actor y is null
             * actors are different references
             * 
             */
        }

        public class TheGetEnumerator
        {

        }

        public class TheCompareToMethod
        {

        }

        public class TheGetHashCodeMethod
        {

        }

        public class TheToStringMethod
        {

        }

        public class TheGetSupportedAssemblies
        {

        }

        public class TheGetSupportUseCases
        {

        }
    }
}

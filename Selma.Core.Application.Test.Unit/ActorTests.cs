using MediatR;
using NSubstitute;
using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;
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
            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsNull()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                object actor = Substitute.ForPartsOf<Actor>(mediator);
                object other = null;
                bool actual;

                // Act
                actual = actor.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsDifferentType()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                object actor = Substitute.ForPartsOf<Actor>(mediator);
                object other = "Not an Actor type";
                bool actual;

                // Act
                actual = actor.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheObjectsAreTheSame()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                object actor = Substitute.ForPartsOf<Actor>(mediator);
                object other = actor;
                bool actual;

                // Act
                actual = actor.Equals(other);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherActorIsDifferentReference()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                IActor other = Substitute.For<IActor>();
                bool actual;

                // Act
                actual = actor.Equals(other);

                // Assert
                Assert.False(actual);
            }

            /* Not testable becuase of the order ReferenceEquals and GetHashCode are checked */
            /*[Fact]
            public void Equals_ReturnsFalse_WhenOtherActorHasDifferentHashCode()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                IActor other = Substitute.For<IActor>();
                bool actual;

                // Act
                actual = actor.Equals(other);

                // Assert
                Assert.False(actual);
            }*/

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherActorIsNull()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                IActor other = null;
                bool actual;

                // Act
                actual = actor.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheActorsAreTheSame()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                IActor other = actor;
                bool actual;

                // Act
                actual = actor.Equals(other);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetEnumerator
        {
            [Fact]
            public void GetEnumerator_ReturnsAnEnumerator_WhenActorIsExplicitelyIEnumerable()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                IEnumerable actor = Substitute.ForPartsOf<Actor>(null, mediator);
                IEnumerator enumerator;

                // Act
                enumerator = actor.GetEnumerator();

                // Assert
                Assert.NotNull(enumerator);
            }

            [Fact]
            public void GetEnumerator_ReturnsAnEnumerator_Allways()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(null, mediator);
                IEnumerator<IActor> enumerator;

                // Act
                enumerator = actor.GetEnumerator();

                // Assert
                Assert.NotNull(enumerator);
            }

            [Fact]
            public void GetEnumerator_ItratesOnlyActorObject_IfNotSuccessor()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(null, mediator);
                IEnumerator<IActor> enumerator;
                IActor actual;

                // Act
                enumerator = actor.GetEnumerator();
                actual = enumerator.Current;

                // Assert
                Assert.Equal(actor, actual);
            }

            [Fact]
            public void GetEnumerator_ItratesToSuccessor_IfActorHasSuccessor()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actor = Substitute.ForPartsOf<Actor>(successor, mediator);
                IEnumerator<IActor> enumerator;
                IActor actual;

                // Act
                enumerator = actor.GetEnumerator();
                enumerator.MoveNext();
                actual = enumerator.Current;

                // Assert
                Assert.Equal(successor, actual);
            }
        }

        public class TheCompareToMethod
        {
            [Fact]
            public void CompareTo_ReturnsNegativeOne_WhenTheOtherActorIsDownTheSuccessorChain()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actor = Substitute.ForPartsOf<Actor>(successor, mediator);
                int expected = 1, actual;

                // Act
                actual = actor.CompareTo(successor);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CompareTo_ReturnsPositiveOne_WhenTheOtherActorIsUpTheSuccessorChain()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actor = Substitute.ForPartsOf<Actor>(successor, mediator);
                int expected = -1, actual;

                // Act
                actual = successor.CompareTo(actor);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CompareTo_ReturnsZero_IfTheSetsAreDisjoint()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                int expected = 0, actual;

                // Act
                actual = actor.CompareTo(successor);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CompareTo_ReturnsZero_IfTheyAreEqual()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                Actor successor = actor;
                int expected = 0, actual;

                // Act
                actual = actor.CompareTo(successor);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CompareTo_ReturnsNegativeOne_WhenTheOtherActorIsDownTheSuccessorChainAsObject()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actor = Substitute.ForPartsOf<Actor>(successor, mediator);
                int expected = 1, actual;

                // Act
                actual = actor.CompareTo(successor as object);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CompareTo_ReturnsPositiveOne_WhenTheOtherActorIsUpTheSuccessorChainAsObject()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actor = Substitute.ForPartsOf<Actor>(successor, mediator);
                int expected = -1, actual;

                // Act
                actual = successor.CompareTo(actor as object);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CompareTo_ReturnsZero_IfTheSetsAreDisjointAsObject()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                int expected = 0, actual;

                // Act
                actual = actor.CompareTo(successor as object);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void CompareTo_ReturnsZero_IfTheyAreEqualAsObject()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actor = Substitute.ForPartsOf<Actor>(mediator);
                Actor successor = actor;
                int expected = 0, actual;

                // Act
                actual = actor.CompareTo(successor as object);

                // Assert
                Assert.Equal(expected, actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            [Fact]
            public void GetHashCode_IsInfluencedByTheSuccessor_IfTheActorHasASuccessor()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor successor = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(successor, mediator);
                int hashA, hashB;

                // Act
                hashA = actorA.GetHashCode();
                hashB = actorB.GetHashCode();

                // Assert
                Assert.NotEqual(hashA, hashB);
            }
        }

        public class TheOperators
        {
            [Fact]
            public void EqualEqual_ReturnsFalse_IfLeftIsNUll()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = default;
                Actor actorB = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actorA == actorB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfRightIsNUll()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = default;
                bool actual;

                // Act
                actual = actorA == actorB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfActorsAreNotEqual()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actorA == actorB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfActorsAreEqual()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = actorA;
                bool actual;

                // Act
                actual = actorA == actorB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfLeftIsNUll()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = default;
                Actor actorB = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actorA != actorB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfRightIsNUll()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = default;
                bool actual;

                // Act
                actual = actorA != actorB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfActorsAreNotEqual()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actorA != actorB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfActorsAreEqual()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = actorA;
                bool actual;

                // Act
                actual = actorA != actorB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void StrictlyLessThan_ReturnsTrue_IfCompareToIsNegative()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(actorA, mediator);
                bool actual;

                // Act
                actual = actorA < actorB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void StrictlyLessThan_ReturnsFalse_IfCompareToIsZero()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actorA < actorB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void LessThanOrEqual_ReturnsTrue_IfCompareToIsNegative()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(actorA, mediator);
                bool actual;

                // Act
                actual = actorA <= actorB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void LessThanOrEqual_ReturnsTrue_IfActorsAreEqual()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = actorA;
                bool actual;

                // Act
                actual = actorA <= actorB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void StrictlyGreaterThan_ReturnsTrue_IfCompareToIsNegative()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(actorA, mediator);
                bool actual;

                // Act
                actual = actorB > actorA;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void StrictlyGreaterThan_ReturnsFalse_IfCompareToIsZero()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(mediator);
                bool actual;

                // Act
                actual = actorB > actorA;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void GreaterThanOrEqual_ReturnsTrue_IfCompareToIsNegative()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = Substitute.ForPartsOf<Actor>(actorA, mediator);
                bool actual;

                // Act
                actual = actorB >= actorA;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void GreaterThanOrEqual_ReturnsTrue_IfActorsAreEqual()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                Actor actorA = Substitute.ForPartsOf<Actor>(mediator);
                Actor actorB = actorA;
                bool actual;

                // Act
                actual = actorB >= actorA;

                // Assert
                Assert.True(actual);
            }
        }
    }
}

using MediatR;
using NSubstitute;
using Selma.Core.Application.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

/* virtual IEnumerable<Assembly> GetSupportedAssemblies()
 * virtual IEnumerable<Type> GetSupportedUseCases()
 */

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
            public class TestUseCaseResponse
            { }
            public interface ITestUseCaseRequest
                : IUseCaseRequest<TestUseCaseResponse>
            { }
            public interface ITestUseCase1
                : IUseCase<ITestUseCaseRequest, TestUseCaseResponse>
            { }
            public interface ITestUseCase2
                : IUseCase<ITestUseCaseRequest, TestUseCaseResponse>
            { }

            public class ActorWithUseCase1Supported
                : Actor
            {
                public ActorWithUseCase1Supported(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Type> GetSupportedUseCases()
                {
                    yield return typeof(ITestUseCase1);
                }
            }

            public class ActorWithNoUseCases
                : Actor
            {
                public ActorWithNoUseCases(IMediator mediator)
                    : base(mediator)
                { }
            }

            [Fact]
            public async void Do_InvokesTheSendForTheMEdiatorInTheActor_IfTheUseCaseIsSupported()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                ITestUseCaseRequest request = Substitute.For<ITestUseCaseRequest>();
                ActorWithUseCase1Supported actor;

                // Act
                actor = new ActorWithUseCase1Supported(mediator);
                await actor.Do(request);

                // Assert
                await mediator.Received().Send(Arg.Any<object>());
            }

            [Fact]
            public async void Do_ThrowsInvalidOperationException_IfBothTheActorAndSuccessorDoesNotSupportTheUseCaseRequest()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                ITestUseCaseRequest request = Substitute.For<ITestUseCaseRequest>();
                InvalidOperationException exception;
                ActorWithNoUseCases actor;

                // Act
                actor = new ActorWithNoUseCases(mediator);
                exception = await Assert.ThrowsAsync<InvalidOperationException>(() => actor.Do(request).AsTask());

                // Assert
                Assert.NotNull(exception);
            }

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

        public class TheGetSupportedAssembliesMethod
        {
            public class TestUseCaseResponse
            { }
            public interface ITestUseCaseRequest
                : IUseCaseRequest<TestUseCaseResponse>
            { }
            public interface ITestUseCase1
                : IUseCase<ITestUseCaseRequest, TestUseCaseResponse>
            { }
            public interface ITestUseCase2
                : IUseCase<ITestUseCaseRequest, TestUseCaseResponse>
            { }

            public class AssemblyWithUseCase
                : Assembly
            {
                public override Type[] GetTypes()
                    => new Type[] { typeof(ITestUseCase1) };
            }

            public class AssemblyWithNoUseCase
                : Assembly
            {
                public override Type[] GetTypes()
                    => new Type[] { typeof(ActorTests) };
            }

            public class AssemblyWithDuplicateUseCase
                : Assembly
            {
                public override Type[] GetTypes()
                    => new Type[] { typeof(ITestUseCase1), typeof(ITestUseCase1) };
            }

            public class AssemblyWithMultipleDuplicateUseCase
                : Assembly
            {
                public override Type[] GetTypes()
                    => new Type[] { typeof(ITestUseCase1), typeof(ITestUseCase1), typeof(ITestUseCase2), typeof(ITestUseCase2) };
            }

            public class AssemblyWithDefaultType
                : Assembly
            {
                public override Type[] GetTypes()
                    => new Type[] { default };
            }

            /* Disallow default assemblies
             * Disallow assemblies without UseCases
             * Disallow assemblies without any types
             * Allow empty IEnumerable of assemblies
             * Allow an assembly with UseCase(s)
             */

            public class ActorWithUseCaseInAssembly
                : Actor
            {
                public ActorWithUseCaseInAssembly(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Assembly> GetSupportedAssemblies()
                {
                    yield return new AssemblyWithUseCase();
                }
            }

            public class ActorWithDefaultAssembly
                : Actor
            {
                public ActorWithDefaultAssembly(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Assembly> GetSupportedAssemblies()
                    => Enumerable.Empty<Assembly>();
            }

            public class ActorWithNoUseCaseInAssembly
                : Actor
            {
                public ActorWithNoUseCaseInAssembly(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Assembly> GetSupportedAssemblies()
                {
                    yield return new AssemblyWithNoUseCase();
                }
            }

            public class ActorWithDuplicateSupportedUseCases
                : Actor
            {
                public ActorWithDuplicateSupportedUseCases(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Assembly> GetSupportedAssemblies()
                {
                    yield return new AssemblyWithDuplicateUseCase();
                }
            }

            public class ActorWithDefaultTypeInASupportedAssembly
                : Actor
            {
                public ActorWithDefaultTypeInASupportedAssembly(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Assembly> GetSupportedAssemblies()
                {
                    yield return new AssemblyWithDefaultType();
                }
            }

            public class ActorWithMultipleDuplicateAssemblyUseCases
                : Actor
            {
                public ActorWithMultipleDuplicateAssemblyUseCases(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Assembly> GetSupportedAssemblies()
                {
                    yield return new AssemblyWithMultipleDuplicateUseCase();
                }
            }

            [Fact]
            public void GetSupportedAssemblies_DoesNotThrowAnException_IfTheActorHasdefinedAnAssemblyWithAUseCase()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();

                // Act
                new ActorWithUseCaseInAssembly(mediator);
            }

            [Fact]
            public void GetSupportedAssemblies_DoesNotThrowAnException_IfTheActorHasDefinedItsSupportedAssembliesAsTheEmptySet()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();

                // Act
                new ActorWithDefaultAssembly(mediator);
            }

            [Fact]
            public void GetSupportedAssemblies_DoesNotThrowAnException_IfASupportedUseCaseFromTheAssemblyIsNotAUseCase()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();

                // act
                new ActorWithNoUseCaseInAssembly(mediator);
            }

            [Fact]
            public void GetSupportedAssemblies_ThrowsInvalidOperationException_IfTheActorHasDuplicateSupportedUseCases()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                InvalidOperationException exception;

                // Act
                exception = Assert.Throws<InvalidOperationException>(() => new ActorWithDuplicateSupportedUseCases(mediator));

                // Assert
                Assert.NotNull(exception);
            }

            [Fact]
            public void GetSupportedAssemblies_DoesNotThrowAnException_IfASupprotedTypeInTheAssemblyIsDefault()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();

                // Act
                new ActorWithDefaultTypeInASupportedAssembly(mediator);
            }

            [Fact]
            public void GetSupportedAssemblies_ThrowsAnAggregateExceptionWithAllInvalidOperationExceptionsFor_IfTheActorHasMulætipleDifferentTypesOfDuiplicateUseCases()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                AggregateException exception;

                // Act
                exception = Assert.Throws<AggregateException>(() => new ActorWithMultipleDuplicateAssemblyUseCases(mediator));

                // Assert
                Assert.NotNull(exception);
            }
        }

        public class TheGetSupportedUseCasesMethod
        {
            public class TestUseCaseResponse
            { }
            public interface ITestUseCaseRequest
                : IUseCaseRequest<TestUseCaseResponse>
            { }
            public interface ITestUseCase1
                : IUseCase<ITestUseCaseRequest, TestUseCaseResponse>
            { }
            public interface ITestUseCase2
                : IUseCase<ITestUseCaseRequest, TestUseCaseResponse>
            { }

            public class ActorWithDefaultUseCaseType
                : Actor
            {
                public ActorWithDefaultUseCaseType(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Type> GetSupportedUseCases()
                {
                    yield return default;
                }
            }

            public class ActorWithNotAUseCaseSupport
                : Actor
            {
                public ActorWithNotAUseCaseSupport(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Type> GetSupportedUseCases()
                {
                    yield return typeof(ActorTests);
                }
            }

            public class ActorWithBothANonUseCaseAndDefaultUseCaseType
                : Actor
            {
                public ActorWithBothANonUseCaseAndDefaultUseCaseType(IMediator mediator)
                    : base(mediator)
                { }

                public override IEnumerable<Type> GetSupportedUseCases()
                {
                    yield return default;
                    yield return typeof(ActorTests);
                }
            }

            [Fact]
            public void GetSupportedUseCases_ThrowsNullReference_IfTheActorSupportsANullUseCaseType()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                NullReferenceException exception;

                // Act
                exception = Assert.Throws<NullReferenceException>(() => new ActorWithDefaultUseCaseType(mediator));

                // Assert
                Assert.NotNull(exception);
            }

            [Fact]
            public void GetSupportedUseCases_ThrowsInvalidOperationException_IfActorSupportedUseCaseIsNotAUseCase()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                InvalidOperationException exception;

                // Act
                exception = Assert.Throws<InvalidOperationException>(() => new ActorWithNotAUseCaseSupport(mediator));

                // Assert
                Assert.NotNull(exception);
            }

            [Fact]
            public void GetSupportedUseCases_ThrowsAggregateException_IfMultipleErrorsWasFound()
            {
                // Arrange
                IMediator mediator = Substitute.For<IMediator>();
                AggregateException exception;

                // Act
                exception = Assert.Throws<AggregateException>(() => new ActorWithBothANonUseCaseAndDefaultUseCaseType(mediator));

                // Assert
                Assert.NotNull(exception);
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

using MediatR;
using NSubstitute;
using Selma.Core.Domain.Events.Abstractions;
using Xunit;

namespace Selma.Core.Domain.Events.Test.Unit
{
    public class DomainEventHandlerTests
    {
        public class TheEqualsMethod
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent()
                    : base()
                { }
            }

            public abstract class AbstractDomainEventHandler
                : DomainEventHandler<ConcreteDomainEvent>
                , IDomainEventHandler<ConcreteDomainEvent>
                , INotification
            { }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsNull()
            {
                // Arrange
                object domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                object domainEventHandlerB = default;
                bool actual;

                // Act
                actual = domainEventHandlerA.Equals(domainEventHandlerB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsDifferentType()
            {
                // Arrange
                object domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                object domainEventHandlerB = 123;
                bool actual;

                // Act
                actual = domainEventHandlerA.Equals(domainEventHandlerB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheObjectsAreTheSame()
            {
                // Arrange
                object domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                object domainEventHandlerB = domainEventHandlerA;
                bool actual;

                // Act
                actual = domainEventHandlerA.Equals(domainEventHandlerB);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherDomainEventHandlerIsDifferentReference()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                bool actual;

                // Act
                actual = domainEventHandlerA.Equals(domainEventHandlerB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherDomainEventHandlerIsNull()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = default;
                bool actual;

                // Act
                actual = domainEventHandlerA.Equals(domainEventHandlerB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheDomainEventHandlerAreTheSame()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = domainEventHandlerA;
                bool actual;

                // Act
                actual = domainEventHandlerA.Equals(domainEventHandlerB);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent()
                    : base()
                { }
            }

            public abstract class AbstractDomainEventHandler
                : DomainEventHandler<ConcreteDomainEvent>
                , IDomainEventHandler<ConcreteDomainEvent>
                , INotification
            { }

            [Fact]
            public void GetHashCode_ReturnsTheSame_IfTheRuntimeInstancesAreTheSame()
            {
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = domainEventHandlerA;
                int hashA, hashB;

                // Act
                hashA = domainEventHandlerA.GetHashCode();
                hashB = domainEventHandlerB.GetHashCode();

                // Assert
                Assert.Equal(hashA, hashB);
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentValues_IfTheRuntimeInstancesAreTheDifferent()
            {
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                int hashA, hashB;

                // Act
                hashA = domainEventHandlerA.GetHashCode();
                hashB = domainEventHandlerB.GetHashCode();

                // Assert
                Assert.NotEqual(hashA, hashB);
            }
        }

        public class TheOperators
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent()
                    : base()
                { }
            }

            public abstract class AbstractDomainEventHandler
                : DomainEventHandler<ConcreteDomainEvent>
                , IDomainEventHandler<ConcreteDomainEvent>
                , INotification
            { }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfLeftIsNull()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = default;
                AbstractDomainEventHandler domainEventHandlerB = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                bool actual;

                // Act
                actual = domainEventHandlerA == domainEventHandlerB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfRightIsNull()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = default;
                bool actual;

                // Act
                actual = domainEventHandlerA == domainEventHandlerB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfActorsAreNotEqual()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                bool actual;

                // Act
                actual = domainEventHandlerA == domainEventHandlerB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfActorsAreEqual()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = domainEventHandlerA;
                bool actual;

                // Act
                actual = domainEventHandlerA == domainEventHandlerB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfLeftIsNull()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = default;
                AbstractDomainEventHandler domainEventHandlerB = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                bool actual;

                // Act
                actual = domainEventHandlerA != domainEventHandlerB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfRightIsNull()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = default;
                bool actual;

                // Act
                actual = domainEventHandlerA != domainEventHandlerB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfActorsAreNotEqual()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                bool actual;

                // Act
                actual = domainEventHandlerA != domainEventHandlerB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfActorsAreEqual()
            {
                // Arrange
                AbstractDomainEventHandler domainEventHandlerA = Substitute.ForPartsOf<AbstractDomainEventHandler>();
                AbstractDomainEventHandler domainEventHandlerB = domainEventHandlerA;
                bool actual;

                // Act
                actual = domainEventHandlerA != domainEventHandlerB;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

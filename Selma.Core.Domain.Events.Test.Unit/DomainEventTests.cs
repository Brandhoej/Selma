using System;
using NSubstitute;
using Selma.Core.Domain.Events.Abstractions;
using Selma.Core.MessageQueue.Abstractions;
using Xunit;

namespace Selma.Core.Domain.Events.Test.Unit
{
    public class DomainEventTests
    {
        public class TheConstructorMethod
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent()
                    : base()
                { }

                public ConcreteDomainEvent(IMessageQueueProducer<IDomainEvent> producer)
                    : base(producer)
                { }
            }

            [Fact]
            public void Constructor_ThrowsArgumentNullException_IfTheSharedProducerIsNull()
            {
                // Arrange
                ArgumentNullException argumentNullException;

                // Act
                argumentNullException = Assert.Throws<ArgumentNullException>(() => new ConcreteDomainEvent());

                // Assert
                Assert.NotNull(argumentNullException);
            }

            [Fact]
            public void Constructor_ThrowsArgumentNullException_IfTheProducerParameterIsNull()
            {
                // Arrange
                ArgumentNullException argumentNullException;

                // Act
                argumentNullException = Assert.Throws<ArgumentNullException>(() => new ConcreteDomainEvent(default));

                // Assert
                Assert.NotNull(argumentNullException);
            }
        }

        public class TheEnqueueMethod
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent(IMessageQueueProducer<IDomainEvent> producer)
                    : base(producer)
                { }
            }

            [Fact]
            public void Enqueue_CallsEnqueueWithTheDomainEvent_WithTheDomainEventAsParameter()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEvent = new ConcreteDomainEvent(producer);

                // Act
                domainEvent.Enqueue();

                // Assert
                producer.Received(1).Enqueue(Arg.Any<ConcreteDomainEvent>());
            }
        }

        public class TheEqualsMethod
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent(IMessageQueueProducer<IDomainEvent> producer)
                    : base(producer)
                { }
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsNull()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                object domainEvent = new ConcreteDomainEvent(producer);
                object other = new ConcreteDomainEvent(producer);
                bool actual;

                // Act
                actual = domainEvent.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsDifferentType()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                object domainEvent = new ConcreteDomainEvent(producer);
                object other = 123;
                bool actual;

                // Act
                actual = domainEvent.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheObjectsAreTheSame()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                object domainEvent = new ConcreteDomainEvent(producer);
                object other = domainEvent;
                bool actual;

                // Act
                actual = domainEvent.Equals(other);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherDomainEventIsDifferentReference()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                IDomainEvent domainEvent = new ConcreteDomainEvent(producer);
                IDomainEvent other = new ConcreteDomainEvent(producer);
                bool actual;

                // Act
                actual = domainEvent.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherDomainEventIsNull()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                IDomainEvent domainEvent = new ConcreteDomainEvent(producer);
                IDomainEvent other = default;
                bool actual;

                // Act
                actual = domainEvent.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheDomainEventAreTheSame()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                IDomainEvent domainEvent = new ConcreteDomainEvent(producer);
                IDomainEvent other = domainEvent;
                bool actual;

                // Act
                actual = domainEvent.Equals(other);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent(IMessageQueueProducer<IDomainEvent> producer)
                    : base(producer)
                { }
            }

            [Fact]
            public void GetHashCode_IsInfluencedByTheProducer_IsNotTheSameWithDifferentProducers()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producerA = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                IMessageQueueProducer<IDomainEvent> producerB = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                IDomainEvent domainEventA = new ConcreteDomainEvent(producerA);
                IDomainEvent domainEventB = new ConcreteDomainEvent(producerB);
                int hashA, hashB;

                // Act
                hashA = domainEventA.GetHashCode();
                hashB = domainEventB.GetHashCode();

                // Assert
                Assert.NotEqual(hashA, hashB);
            }

            [Fact]
            public void GetHashCode_IsAlsoInfluencedByTheRuntimeInstance_IfTheReferencesAreNotEqualTheHashCodesAreNotEqual()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                IDomainEvent domainEventA = new ConcreteDomainEvent(producer);
                IDomainEvent domainEventB = new ConcreteDomainEvent(producer);
                int hashA, hashB;

                // Act
                hashA = domainEventA.GetHashCode();
                hashB = domainEventB.GetHashCode();

                // Assert
                Assert.NotEqual(hashA, hashB);
            }
        }

        public class TheOperators
        {
            public class ConcreteDomainEvent
                : DomainEvent
            {
                public ConcreteDomainEvent(IMessageQueueProducer<IDomainEvent> producer)
                    : base(producer)
                { }
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfLeftIsNull()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = default;
                DomainEvent domainEventB = new ConcreteDomainEvent(producer);
                bool actual;

                // Act
                actual = domainEventA == domainEventB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfRightIsNull()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = new ConcreteDomainEvent(producer);
                DomainEvent domainEventB = default;
                bool actual;

                // Act
                actual = domainEventA == domainEventB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfActorsAreNotEqual()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = new ConcreteDomainEvent(producer);
                DomainEvent domainEventB = new ConcreteDomainEvent(producer);
                bool actual;

                // Act
                actual = domainEventA == domainEventB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfActorsAreEqual()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = new ConcreteDomainEvent(producer);
                DomainEvent domainEventB = domainEventA;
                bool actual;

                // Act
                actual = domainEventA == domainEventB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfLeftIsNull()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = default;
                DomainEvent domainEventB = new ConcreteDomainEvent(producer);
                bool actual;

                // Act
                actual = domainEventA != domainEventB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfRightIsNull()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = new ConcreteDomainEvent(producer);
                DomainEvent domainEventB = default;
                bool actual;

                // Act
                actual = domainEventA != domainEventB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfActorsAreNotEqual()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = new ConcreteDomainEvent(producer);
                DomainEvent domainEventB = new ConcreteDomainEvent(producer);
                bool actual;

                // Act
                actual = domainEventA != domainEventB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfActorsAreEqual()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEventA = new ConcreteDomainEvent(producer);
                DomainEvent domainEventB = domainEventA;
                bool actual;

                // Act
                actual = domainEventA != domainEventB;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

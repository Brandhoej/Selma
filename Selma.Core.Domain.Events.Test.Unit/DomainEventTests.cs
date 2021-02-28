using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NSubstitute;
using Selma.Core.Domain.Events.Abstractions;
using Selma.Core.MessageQueue;
using Selma.Core.MessageQueue.Abstractions;
using Selma.Core.MessageQueue.MediatR;
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
                public ConcreteDomainEvent()
                    : base()
                { }

                public ConcreteDomainEvent(IMessageQueueProducer<IDomainEvent> producer)
                    : base(producer)
                { }
            }

            [Fact]
            public void Test()
            {
                // Arrange
                IMessageQueueProducer<IDomainEvent> producer = Substitute.For<IMessageQueueProducer<IDomainEvent>>();
                DomainEvent domainEvent = new ConcreteDomainEvent(producer);

                // Act
                domainEvent.Enqueue();

                // Assert
                producer.Received(1).Enqueue(domainEvent);
            }
        }

        public class TheEqualsMethod
        {

        }

        public class TheGetHashCodeMethod
        {

        }

        public class TheOperators
        {

        }
    }
}

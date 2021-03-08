using NSubstitute;
using Selma.Core.Domain.Abstractions;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using Xunit;

namespace Selma.Core.Infrastructure.Persistent.Test.Unit
{
    public class UnitOfWorkTests
    {
        public class TheConstructor
        {
            [Fact]
            public void Constructor_ThrowsArgumentNullException_IfContextIsNull()
            {
                // Arrange
                IContext context = default;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWork;
                ArgumentException argumentException;

                // Act
                argumentException = Assert.Throws<ArgumentNullException>(() => unitOfWork = new UnitOfWork<IContext>(context, abstractRepositoryFactory));

                // Assert
                Assert.NotNull(argumentException);
            }

            [Fact]
            public void Constructor_ThrowsArgumentNullException_IfAbstractRepositoryFactoryIsNull()
            {
                // Arrange
                IContext context = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactory = default;
                UnitOfWork<IContext> unitOfWork;
                ArgumentException argumentException;

                // Act
                argumentException = Assert.Throws<ArgumentNullException>(() => unitOfWork = new UnitOfWork<IContext>(context, abstractRepositoryFactory));

                // Assert
                Assert.NotNull(argumentException);
            }
        }

        public class TheSaveChangesMethod
        {
            [Fact]
            public void SaveChanges_CallsSaveChangesOnTheContext_WhenCalled()
            {
                // Arrange
                IContext context = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWork = new UnitOfWork<IContext>(context, abstractRepositoryFactory);

                // Act
                unitOfWork.SaveChanges();

                // Assert
                context.Received(1).SaveChanges();
            }
        }

        public class TheSaveChangesAsyncMethod
        {
            [Fact]
            public void SaveChanges_CallsSaveChangesAsyncOnTheContext_WhenCalledWithoutCancellationToken()
            {
                // Arrange
                IContext context = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWork = new UnitOfWork<IContext>(context, abstractRepositoryFactory);

                // Act
                unitOfWork.SaveChangesAsync();

                // Assert
                context.Received(1).SaveChangesAsync();
            }

            [Fact]
            public void SaveChanges_CallsSaveChangesAsyncOnTheContext_WhenCalledWithCancellationToken()
            {
                // Arrange
                IContext context = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                CancellationToken cancellationToken = CancellationToken.None;
                UnitOfWork<IContext> unitOfWork = new UnitOfWork<IContext>(context, abstractRepositoryFactory);

                // Act
                unitOfWork.SaveChangesAsync(cancellationToken);

                // Assert
                context.Received(1).SaveChangesAsync(cancellationToken);
            }
        }

        public class TheEqualsMethod
        {
            [Fact]
            public void Equals_ReturnsFalse_WhenOtherObjectIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                object unitOfWorkR = default;

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenContextAbstractRepositoryFactoryAndObjectReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenAbstractRepositoryFactoryAndObjectReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenContextAndObjectReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                object unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenObjectReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                object unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherObjectIsDifferentType()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                object unitOfWorkR = "Not a UnitOfWork Type ;)";

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenObjectReferencesAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                object unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                object unitOfWorkR = unitofWorkL;

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                UnitOfWork<IContext> unitOfWorkR = default;

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenContextAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenContextAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenReferencesAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                UnitOfWork<IContext> unitOfWorkR = unitofWorkL;

                bool actual;

                // Act
                actual = unitofWorkL.Equals(unitOfWorkR);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            [Fact]
            public void GetHashCode_ReturnsDifferentHashCodes_IfContextAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                int hashCodeL;

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);
                int hashCodeR;
                bool theSameHashCode;

                // Act
                hashCodeL = unitofWorkL.GetHashCode();
                hashCodeR = unitOfWorkR.GetHashCode();
                theSameHashCode = hashCodeL == hashCodeR;

                // Assert
                Assert.False(theSameHashCode);
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentHashCodes_IfAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                int hashCodeL;

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);
                int hashCodeR;
                bool theSameHashCode;

                // Act
                hashCodeL = unitofWorkL.GetHashCode();
                hashCodeR = unitOfWorkR.GetHashCode();
                theSameHashCode = hashCodeL == hashCodeR;

                // Assert
                Assert.False(theSameHashCode);
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentHashCodes_IfContextAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                int hashCodeL;

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);
                int hashCodeR;
                bool theSameHashCode;

                // Act
                hashCodeL = unitofWorkL.GetHashCode();
                hashCodeR = unitOfWorkR.GetHashCode();
                theSameHashCode = hashCodeL == hashCodeR;

                // Assert
                Assert.False(theSameHashCode);
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentHashCodes_IfReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                int hashCodeL;

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);
                int hashCodeR;
                bool theSameHashCode;

                // Act
                hashCodeL = unitofWorkL.GetHashCode();
                hashCodeR = unitOfWorkR.GetHashCode();
                theSameHashCode = hashCodeL == hashCodeR;

                // Assert
                Assert.False(theSameHashCode);
            }

            [Fact]
            public void GetHashCode_ReturnsTheSameHashCodes_IfReferencesAreTheSame()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                int hashCodeL;

                UnitOfWork<IContext> unitOfWorkR = unitofWorkL;
                int hashCodeR;
                bool theSameHashCode;

                // Act
                hashCodeL = unitofWorkL.GetHashCode();
                hashCodeR = unitOfWorkR.GetHashCode();
                theSameHashCode = hashCodeL == hashCodeR;

                // Assert
                Assert.True(theSameHashCode);
            }
        }

        public class TheRepositoryMethod
        {
            public abstract class ConcreteEntityGuid
                : IEntityRoot
            {
                public abstract Guid Id { get; }
                public abstract bool Equals([AllowNull] IEntity<Guid> other);
                public abstract bool Equals([AllowNull] IEntityRoot<Guid> other);
            }

            [Fact]
            public void Repository_CallesRepositoryOnTheAbstractRepositoryFactory_IfCalled()
            {
                // Arrange
                IContext context = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                IUnitOfWork unitofWork = new UnitOfWork<IContext>(context, abstractRepositoryFactory);

                // Act
                unitofWork.Repository<ConcreteEntityGuid>();

                // Assert
                abstractRepositoryFactory.Received(1).Repository<ConcreteEntityGuid>();
            }

            [Fact]
            public void Repository_CallesRepositoryOnTheAbstractRepositoryFactory_IfCalledWithGernericId()
            {
                // Arrange
                IContext context = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                IUnitOfWork unitofWork = new UnitOfWork<IContext>(context, abstractRepositoryFactory);

                // Act
                unitofWork.Repository<ConcreteEntityGuid, Guid>();

                // Assert
                abstractRepositoryFactory.Received(1).Repository<ConcreteEntityGuid, Guid>();
            }
        }

        public class TheOperators
        {
            [Fact]
            public void EqualEqual_ReturnsFalse_WhenRightIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                UnitOfWork<IContext> unitOfWorkR = default;

                bool actual;

                // Act
                actual = unitofWorkL == unitOfWorkR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenLeftIsNull()
            {
                // Arrange
                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);
                UnitOfWork<IContext> unitofWorkL = default;

                bool actual;

                // Act
                actual = unitofWorkL == unitOfWorkR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenContextAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL == unitOfWorkR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL == unitOfWorkR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenContextAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL == unitOfWorkR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL == unitOfWorkR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfTheUnitOfWorksAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                UnitOfWork<IContext> unitOfWorkR = unitofWorkL;

                bool actual;

                // Act
                actual = unitofWorkL == unitOfWorkR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenRightIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                UnitOfWork<IContext> unitOfWorkR = default;

                bool actual;

                // Act
                actual = unitofWorkL != unitOfWorkR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenLeftIsNull()
            {
                // Arrange
                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);
                UnitOfWork<IContext> unitofWorkL = default;

                bool actual;

                // Act
                actual = unitofWorkL != unitOfWorkR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenContextAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL != unitOfWorkR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenAbstractRepositoryFactoryAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL != unitOfWorkR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenContextAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL != unitOfWorkR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);

                IContext contextR = contextL;
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                UnitOfWork<IContext> unitOfWorkR = new UnitOfWork<IContext>(contextR, abstractRepositoryFactoryR);

                bool actual;

                // Act
                actual = unitofWorkL != unitOfWorkR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfTheUnitOfWorksAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                IAbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.For<IAbstractRepositoryFactory<IContext>>();
                UnitOfWork<IContext> unitofWorkL = new UnitOfWork<IContext>(contextL, abstractRepositoryFactoryL);
                UnitOfWork<IContext> unitOfWorkR = unitofWorkL;

                bool actual;

                // Act
                actual = unitofWorkL != unitOfWorkR;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

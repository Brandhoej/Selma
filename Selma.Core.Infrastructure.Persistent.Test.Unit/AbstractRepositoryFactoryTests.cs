using NSubstitute;
using Selma.Core.Infrastructure.Persistent.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Selma.Core.Infrastructure.Persistent.Test.Unit
{
    public class AbstractRepositoryFactoryTests
    {
        public class TheConstructor
        {
            public class ConcreteAbstractRepositoryFactory
                : AbstractRepositoryFactory<IContext>
            {
                public ConcreteAbstractRepositoryFactory(IContext context)
                    : base(context)
                { }

                public override IRepository<TEntity> Repository<TEntity>()
                    => throw new NotImplementedException();

                public override IRepository<TEntity, TId> Repository<TEntity, TId>()
                    => throw new NotImplementedException();
            }

            [Fact]
            public void Constructor_ThrowsArgumentNullException_IfContextIsNull()
            {
                // Arrange
                IContext context = default;
                Exception argumentNullException;

                // Act
                argumentNullException = Assert.Throws<ArgumentNullException>(() => new ConcreteAbstractRepositoryFactory(context));

                // Assert
                Assert.NotNull(argumentNullException);
            }
        }

        public class TheEqualsMethod
        {
            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectsAreDifferentTypes()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                object abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                object abstractRepositoryFactoryR = "This is not the correct type...";

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheOtherObjectIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                object abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                object abstractRepositoryFactoryR = default;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheContextsAndObjectReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                object abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = Substitute.For<IContext>();
                object abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                object abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = contextL;
                object abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheObjectReferencesAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                object abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                object abstractRepositoryFactoryR = abstractRepositoryFactoryL;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = default;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheContextsAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = contextL;
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheReferencesAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL.Equals(abstractRepositoryFactoryR);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            [Fact]
            public void GetHashCode_ReturnsDifferentHashCodes_WhenTheContextAndReferenceAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                int hashCodeL;

                IContext contextR = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);
                int hashCodeR;

                bool theSame;

                // Act
                hashCodeL = abstractRepositoryFactoryL.GetHashCode();
                hashCodeR = abstractRepositoryFactoryR.GetHashCode();
                theSame = hashCodeL == hashCodeR;

                // Assert
                Assert.False(theSame);
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentHashCodes_WhenTheReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                int hashCodeL;

                IContext contextR = contextL;
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);
                int hashCodeR;

                bool theSame;

                // Act
                hashCodeL = abstractRepositoryFactoryL.GetHashCode();
                hashCodeR = abstractRepositoryFactoryR.GetHashCode();
                theSame = hashCodeL == hashCodeR;

                // Assert
                Assert.False(theSame);
            }

            [Fact]
            public void GetHashCode_ReturnsTheSameHashCode_WhenTheReferencesAreTheSame()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                int hashCodeL;

                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;
                int hashCodeR;

                bool theSame;

                // Act
                hashCodeL = abstractRepositoryFactoryL.GetHashCode();
                hashCodeR = abstractRepositoryFactoryR.GetHashCode();
                theSame = hashCodeL == hashCodeR;

                // Assert
                Assert.True(theSame);
            }
        }

        public class TheOperators
        {
            [Fact]
            public void EqualEqual_ReturnsFalse_WhenRightIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = default;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL == abstractRepositoryFactoryR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenLeftIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = default;
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL == abstractRepositoryFactoryR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenTheContextsAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL == abstractRepositoryFactoryR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_WhenTheReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = contextL;
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL == abstractRepositoryFactoryR;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_WhenTheReferencesAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL == abstractRepositoryFactoryR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenRightIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = default;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL != abstractRepositoryFactoryR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenLeftIsNull()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = default;
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL != abstractRepositoryFactoryR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenTheContextsAndReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL != abstractRepositoryFactoryR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_WhenTheReferencesAreDifferent()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);

                IContext contextR = contextL;
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextR);

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL != abstractRepositoryFactoryR;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_WhenTheReferencesAreEqual()
            {
                // Arrange
                IContext contextL = Substitute.For<IContext>();
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryL = Substitute.ForPartsOf<AbstractRepositoryFactory<IContext>>(contextL);
                AbstractRepositoryFactory<IContext> abstractRepositoryFactoryR = abstractRepositoryFactoryL;

                bool actual;

                // Act
                actual = abstractRepositoryFactoryL != abstractRepositoryFactoryR;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

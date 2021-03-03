using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Selma.Core.Domain.Test.Unit
{
    public class EntityRootTests
    {
        public class TheConstructorMethod
        {
            [Fact]
            public void Constructor_SetsIdToANonDefaultValue_IfTHeEmptyConstructorWasUsed()
            {
                // Arrange
                EntityRoot entity;

                // Act
                entity = Substitute.ForPartsOf<EntityRoot>();

                // Assert
                Assert.NotEqual(default, entity.Id);
            }

            [Fact]
            public void Constructor_setsIdToThePassedValue_IfAValueWasPassedInTheConstructor()
            {
                // Arrange
                EntityRoot entity;
                Guid guid = Guid.NewGuid();

                // Act
                entity = Substitute.ForPartsOf<EntityRoot>(guid);

                // Assert
                Assert.Equal(guid, entity.Id);
            }
        }

        public class TheEqualsMethod
        {
            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsNullAndWhenIdIsSet()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                object entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                object entityB = default;
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenOtherIsDifferentTypeAndWhenIdIsSet()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                object entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                object entityB = 123;
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectsAredifferentReferencesButTheirIdIsTheSame()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                object entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                object entityB = Substitute.ForPartsOf<EntityRoot>(entityAId);
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectsAredifferentReferencesAndTheirIdIsDifferent()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                Guid entityBId = Guid.NewGuid();
                object entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                object entityB = Substitute.ForPartsOf<EntityRoot>(entityBId);
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheObjectsAreTheSameAndWhenIdIsSet()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                object entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                object entityB = entityA;
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenEntitiesIsNullAndWhenIdIsSet()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                EntityRoot entityB = default;
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheEntitiesAredifferentReferencesButTheirIdIsTheSame()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                EntityRoot entityB = Substitute.ForPartsOf<EntityRoot>(entityAId);
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheEntitiesAredifferentReferencesAndTheirIdIsDifferent()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                Guid entityBId = Guid.NewGuid();
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                EntityRoot entityB = Substitute.ForPartsOf<EntityRoot>(entityBId);
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheEntitiesAreTheSameAndWhenIdIsSet()
            {
                // Arrange
                Guid entityAId = Guid.NewGuid();
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>(entityAId);
                EntityRoot entityB = entityA;
                bool actual;

                // Act
                actual = entityA.Equals(entityB);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            [Fact]
            public void GetHashCode_ReturnsTheSameHashCodeValueAndTypeForTwoEntities_IfTheyHaveTheSameId()
            {
                // Arrange
                Guid guid = Guid.NewGuid();
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>(guid);
                EntityRoot entityB = Substitute.ForPartsOf<EntityRoot>(guid);
                int hashCodeA, hashCodeB;

                // Act
                hashCodeA = entityA.GetHashCode();
                hashCodeB = entityB.GetHashCode();

                // Assert
                Assert.Equal(hashCodeA, hashCodeB);
            }

            [Fact]
            public void GetHashCode_DoesNotReturnTheSameHashCode_IfTypesAreDifferentButValuesTheSame()
            {
                // Arrange
                int hashCode = 1;
                EntityRoot<short> entityA = Substitute.ForPartsOf<EntityRoot<short>>((short)hashCode);
                EntityRoot<int> entityB = Substitute.ForPartsOf<EntityRoot<int>>((int)hashCode);
                int hashCodeA, hashCodeB;

                // Act
                hashCodeA = entityA.GetHashCode();
                hashCodeB = entityB.GetHashCode();

                // Assert
                Assert.NotEqual(hashCodeA, hashCodeB);
            }
        }

        public class TheOperators
        {
            [Fact]
            public void EqualEqual_ReturnsFalse_IfLeftIsNull()
            {
                // Arrange
                EntityRoot entityA = default;
                EntityRoot entityB = Substitute.ForPartsOf<EntityRoot>();
                bool actual;

                // Act
                actual = entityA == entityB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfRightIsNull()
            {
                // Arrange
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>();
                EntityRoot entityB = default;
                bool actual;

                // Act
                actual = entityA == entityB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfEntityRootsAreNotEqual()
            {
                // Arrange
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>();
                EntityRoot entityB = Substitute.ForPartsOf<EntityRoot>();
                bool actual;

                // Act
                actual = entityA == entityB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfEntityRootsAreEqual()
            {
                // Arrange
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>();
                EntityRoot entityB = entityA;
                bool actual;

                // Act
                actual = entityA == entityB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfLeftIsNull()
            {
                // Arrange
                EntityRoot entityA = default;
                EntityRoot entityB = Substitute.ForPartsOf<EntityRoot>();
                bool actual;

                // Act
                actual = entityA != entityB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfRightIsNull()
            {
                // Arrange
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>();
                EntityRoot entityB = default;
                bool actual;

                // Act
                actual = entityA != entityB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfEntityRootsAreNotEqual()
            {
                // Arrange
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>();
                EntityRoot entityB = Substitute.ForPartsOf<EntityRoot>();
                bool actual;

                // Act
                actual = entityA != entityB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfEntityRootsAreEqual()
            {
                // Arrange
                EntityRoot entityA = Substitute.ForPartsOf<EntityRoot>();
                EntityRoot entityB = entityA;
                bool actual;

                // Act
                actual = entityA != entityB;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Selma.Core.Domain.Test.Unit
{
    public class EntityTests
    {
        public class TheConstructorMethod
        {
            [Fact]
            public void Constructor_SetsIdToANonDefaultValue_IfTHeEmptyConstructorWasUsed()
            {
                // Arrange
                Entity entity;

                // Act
                entity = Substitute.ForPartsOf<Entity>();

                // Assert
                Assert.NotEqual(default, entity.Id);
            }

            [Fact]
            public void Constructor_setsIdToThePassedValue_IfAValueWasPassedInTheConstructor()
            {
                // Arrange
                Entity entity;
                Guid guid = Guid.NewGuid();

                // Act
                entity = Substitute.ForPartsOf<Entity>(guid);

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
                object entityA = Substitute.ForPartsOf<Entity>(entityAId);
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
                object entityA = Substitute.ForPartsOf<Entity>(entityAId);
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
                object entityA = Substitute.ForPartsOf<Entity>(entityAId);
                object entityB = Substitute.ForPartsOf<Entity>(entityAId);
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
                object entityA = Substitute.ForPartsOf<Entity>(entityAId);
                object entityB = Substitute.ForPartsOf<Entity>(entityBId);
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
                object entityA = Substitute.ForPartsOf<Entity>(entityAId);
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
                Entity entityA = Substitute.ForPartsOf<Entity>(entityAId);
                Entity entityB = default;
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
                Entity entityA = Substitute.ForPartsOf<Entity>(entityAId);
                Entity entityB = Substitute.ForPartsOf<Entity>(entityAId);
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
                Entity entityA = Substitute.ForPartsOf<Entity>(entityAId);
                Entity entityB = Substitute.ForPartsOf<Entity>(entityBId);
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
                Entity entityA = Substitute.ForPartsOf<Entity>(entityAId);
                Entity entityB = entityA;
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
                Entity entityA = Substitute.ForPartsOf<Entity>(guid);
                Entity entityB = Substitute.ForPartsOf<Entity>(guid);
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
                Entity<short> entityA = Substitute.ForPartsOf<Entity<short>>((short)hashCode);
                Entity<int> entityB = Substitute.ForPartsOf<Entity<int>>((int)hashCode);
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
                Entity entityA = default;
                Entity entityB = Substitute.ForPartsOf<Entity>();
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
                Entity entityA = Substitute.ForPartsOf<Entity>();
                Entity entityB = default;
                bool actual;

                // Act
                actual = entityA == entityB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfEntitiesAreNotEqual()
            {
                // Arrange
                Entity entityA = Substitute.ForPartsOf<Entity>();
                Entity entityB = Substitute.ForPartsOf<Entity>();
                bool actual;

                // Act
                actual = entityA == entityB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfEntitiesAreEqual()
            {
                // Arrange
                Entity entityA = Substitute.ForPartsOf<Entity>();
                Entity entityB = entityA;
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
                Entity entityA = default;
                Entity entityB = Substitute.ForPartsOf<Entity>();
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
                Entity entityA = Substitute.ForPartsOf<Entity>();
                Entity entityB = default;
                bool actual;

                // Act
                actual = entityA != entityB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfEntitiesAreNotEqual()
            {
                // Arrange
                Entity entityA = Substitute.ForPartsOf<Entity>();
                Entity entityB = Substitute.ForPartsOf<Entity>();
                bool actual;

                // Act
                actual = entityA != entityB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfEntitiesAreEqual()
            {
                // Arrange
                Entity entityA = Substitute.ForPartsOf<Entity>();
                Entity entityB = entityA;
                bool actual;

                // Act
                actual = entityA != entityB;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

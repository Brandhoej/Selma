using NSubstitute;
using Selma.Core.Domain.Abstractions;
using System.Collections.Generic;
using Xunit;

namespace Selma.Core.Domain.Test.Unit
{
    public class ValueObjectTests
    {
        public class TheEqualsMethod
        {
            public class ValueObjectInt
                : ValueObject
            {
                public ValueObjectInt(int value)
                    => Value = value;

                public int Value { get; }

                protected override IEnumerable<object> GetEqualityComponents()
                {
                    yield return Value;
                }
            }

            public class ValueObjectShort
                : ValueObject
            {
                public ValueObjectShort(short value)
                    => Value = value;

                public short Value { get; }

                protected override IEnumerable<object> GetEqualityComponents()
                {
                    yield return Value;
                }
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheObjectsOfDifferentValueObjectTypesAreTheSame()
            {
                // Arrange
                object valueObjectInt = new ValueObjectInt(0);
                object valueObjectShort = new ValueObjectShort(0);
                bool actual;

                // Act
                actual = valueObjectInt.Equals(valueObjectShort);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheObjectsOfTheSameTypeValueObjectDiffer()
            {
                // Arrange
                object valueObjectIntA = new ValueObjectInt(0);
                object valueObjectIntB = new ValueObjectInt(1);
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheOtherObjectIsOfDifferentType()
            {
                // Arrange
                object valueObjectIntA = new ValueObjectInt(0);
                object valueObjectIntB = 1;
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_IfTheObjectIsMathcedByItself()
            {
                // Arrange
                object valueObjectInt = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectInt.Equals(valueObjectInt);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheValuesOfTheObjectAndTypeIsTheSame()
            {
                // Arrange
                object valueObjectIntA = new ValueObjectInt(0);
                object valueObjectIntB = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheIValuesOfDifferentValueObjectTypesAreTheSame()
            {
                // Arrange
                IValueObject valueObjectInt = new ValueObjectInt(0);
                IValueObject valueObjectShort = new ValueObjectShort(0);
                bool actual;

                // Act
                actual = valueObjectInt.Equals(valueObjectShort);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheIValuesOfTheSameTypeValueObjectDiffer()
            {
                // Arrange
                IValueObject valueObjectIntA = new ValueObjectInt(0);
                IValueObject valueObjectIntB = new ValueObjectInt(1);
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheOtherIValueObjectIsOfDifferentType()
            {
                // Arrange
                IValueObject valueObjectIntA = new ValueObjectInt(0);
                IValueObject valueObjectIntB = Substitute.For<IValueObject>();
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_IfTheIValueObjectIsMathcedByItself()
            {
                // Arrange
                IValueObject valueObjectInt = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectInt.Equals(valueObjectInt);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheValuesOfTheIValueObjectAndTypeIsTheSame()
            {
                // Arrange
                IValueObject valueObjectIntA = new ValueObjectInt(0);
                IValueObject valueObjectIntB = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheValuesOfDifferentValueObjectTypesAreTheSame()
            {
                // Arrange
                ValueObjectInt valueObjectInt = new ValueObjectInt(0);
                ValueObjectShort valueObjectShort = new ValueObjectShort(0);
                bool actual;

                // Act
                actual = valueObjectInt.Equals(valueObjectShort);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_IfTheValuesOfTheSameTypeValueObjectDiffer()
            {
                // Arrange
                ValueObjectInt valueObjectIntA = new ValueObjectInt(0);
                ValueObjectInt valueObjectIntB = new ValueObjectInt(1);
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_IfTheValueObjectIsMathcedByItself()
            {
                // Arrange
                ValueObjectInt valueObjectInt = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectInt.Equals(valueObjectInt);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheValuesOfTheValueObjectAndTypeIsTheSame()
            {
                // Arrange
                ValueObjectInt valueObjectIntA = new ValueObjectInt(0);
                ValueObjectInt valueObjectIntB = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectIntA.Equals(valueObjectIntB);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            public class ValueObjectInt
                : ValueObject
            {
                public ValueObjectInt(int value)
                    => Value = value;

                public int Value { get; }

                protected override IEnumerable<object> GetEqualityComponents()
                {
                    yield return Value;
                }
            }

            public class ValueObjectShort
                : ValueObject
            {
                public ValueObjectShort(short value)
                    => Value = value;

                public short Value { get; }

                protected override IEnumerable<object> GetEqualityComponents()
                {
                    yield return Value;
                }
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentHashCodesForTheValueAndTheCorrespondingValueObject_IfTheValuesAreEqual()
            {
                // Arrange
                int value = 0;
                ValueObjectInt valueObjectInt = new ValueObjectInt(value);
                int valueHashCode, valueObjectHashCode;

                // Act
                valueHashCode = value.GetHashCode();
                valueObjectHashCode = valueObjectInt.GetHashCode();

                // Assert
                Assert.NotEqual(valueHashCode, valueObjectHashCode);
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentValues_IfTheValueInsideTheValueObjectAreDifferent()
            {
                // Arrange
                ValueObjectInt valueObjectIntA = new ValueObjectInt(0);
                ValueObjectInt ValueObjectIntB = new ValueObjectInt(1);
                int valueObjectIntAHashCode, ValueObjectIntBHashCode;

                // Act
                valueObjectIntAHashCode = valueObjectIntA.GetHashCode();
                ValueObjectIntBHashCode = ValueObjectIntB.GetHashCode();

                // Assert
                Assert.NotEqual(valueObjectIntAHashCode, ValueObjectIntBHashCode);
            }

            [Fact]
            public void GetHashCode_ReturnsDifferentValues_IfTheValueObjectTypeIsDifferentButTheValueIsTheSame()
            {
                // Arrange
                short value = 0;
                ValueObjectInt valueObjectInt = new ValueObjectInt(value);
                ValueObjectShort valueObjectShort = new ValueObjectShort(value);
                int valueObjectIntHashCode, valueObjectShortHashCode;

                // Act
                valueObjectIntHashCode = valueObjectInt.GetHashCode();
                valueObjectShortHashCode = valueObjectShort.GetHashCode();

                // Assert
                Assert.NotEqual(valueObjectIntHashCode, valueObjectShortHashCode);
            }

            [Fact]
            public void GetHashCode_ReturnsTheSameValue_IfTheTypeAndValueAreTheSameButTheReferencesAreNot()
            {
                // Arrange
                short value = 0;
                ValueObjectInt valueObjectIntA = new ValueObjectInt(value);
                ValueObjectInt ValueObjectIntB = new ValueObjectInt(value);
                int valueObjectIntAHashCode, ValueObjectIntBHashCode;

                // Act
                valueObjectIntAHashCode = valueObjectIntA.GetHashCode();
                ValueObjectIntBHashCode = ValueObjectIntB.GetHashCode();

                // Assert
                Assert.Equal(valueObjectIntAHashCode, ValueObjectIntBHashCode);
            }
        }

        public class TheOperators
        {
            public class ValueObjectInt
                : ValueObject
            {
                public ValueObjectInt(int value)
                    => Value = value;

                public int Value { get; }

                protected override IEnumerable<object> GetEqualityComponents()
                {
                    yield return Value;
                }
            }

            public class ValueObjectShort
                : ValueObject
            {
                public ValueObjectShort(short value)
                    => Value = value;

                public short Value { get; }

                protected override IEnumerable<object> GetEqualityComponents()
                {
                    yield return Value;
                }
            }

            [Fact]
            public void EqualsEquals_ReturnsFalse_IfTheValuesOfDifferentValueObjectTypesAreTheSame()
            {
                // Arrange
                ValueObject valueObjectInt = new ValueObjectInt(0);
                ValueObject valueObjectShort = new ValueObjectShort(0);
                bool actual;

                // Act
                actual = valueObjectInt == valueObjectShort;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualsEquals_ReturnsFalse_IfTheValuesOfTheSameTypeValueObjectDiffer()
            {
                // Arrange
                ValueObject valueObjectIntA = new ValueObjectInt(0);
                ValueObject valueObjectIntB = new ValueObjectInt(1);
                bool actual;

                // Act
                actual = valueObjectIntA == valueObjectIntB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualsEquals_ReturnsFalse_WhenLeftSideIsDefault()
            {
                // Arrange
                ValueObject valueObjectIntA = default;
                ValueObject valueObjectIntB = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectIntA == valueObjectIntB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualsEquals_ReturnsFalse_WhenRightSideIsDefault()
            {
                // Arrange
                ValueObject valueObjectIntA = new ValueObjectInt(0);
                ValueObject valueObjectIntB = default;
                bool actual;

                // Act
                actual = valueObjectIntA == valueObjectIntB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualsEquals_ReturnsTrue_WhenTheValuesOfTheValueObjectAndTypeIsTheSame()
            {
                // Arrange
                ValueObject valueObjectIntA = new ValueObjectInt(0);
                ValueObject valueObjectIntB = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectIntA == valueObjectIntB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEquals_ReturnsTrue_IfTheValuesOfDifferentValueObjectTypesAreTheSame()
            {
                // Arrange
                ValueObject valueObjectInt = new ValueObjectInt(0);
                ValueObject valueObjectShort = new ValueObjectShort(0);
                bool actual;

                // Act
                actual = valueObjectInt != valueObjectShort;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEquals_ReturnsTrue_IfTheValuesOfTheSameTypeValueObjectDiffer()
            {
                // Arrange
                ValueObject valueObjectIntA = new ValueObjectInt(0);
                ValueObject valueObjectIntB = new ValueObjectInt(1);
                bool actual;

                // Act
                actual = valueObjectIntA != valueObjectIntB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEquals_ReturnsTrue_WhenLeftSideIsDefault()
            {
                // Arrange
                ValueObject valueObjectIntA = default;
                ValueObject valueObjectIntB = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectIntA != valueObjectIntB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEquals_ReturnsTrue_WhenRightSideIsDefault()
            {
                // Arrange
                ValueObject valueObjectIntA = new ValueObjectInt(0);
                ValueObject valueObjectIntB = default;
                bool actual;

                // Act
                actual = valueObjectIntA != valueObjectIntB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEquals_ReturnsFalse_WhenTheValuesOfTheValueObjectAndTypeIsTheSame()
            {
                // Arrange
                ValueObject valueObjectIntA = new ValueObjectInt(0);
                ValueObject valueObjectIntB = new ValueObjectInt(0);
                bool actual;

                // Act
                actual = valueObjectIntA != valueObjectIntB;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

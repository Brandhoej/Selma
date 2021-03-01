using NSubstitute;
using Xunit;

namespace Selma.Core.Application.Test.Unit
{
    public class UseCaseTests
    {
        public class Response { }
        public class RequestA
            : UseCaseRequest<Response>
        { }
        public class RequestB
            : UseCaseRequest<Response>
        { }

        public class TheEqualsMethod
        {
            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectIsNull()
            {
                // Arrange
                object useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                object other = default;
                bool actual;

                // Act
                actual = useCase.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectIsDifferentType()
            {
                // Arrange
                object useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                object other = 123;
                bool actual;

                // Act
                actual = useCase.Equals(other as object);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectsAreDifferentReferences()
            {
                // Arrange
                object useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                object other = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCase.Equals(other as object);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheObjectsAreTheSame()
            {
                // Arrange
                object useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCase.Equals(useCase);

                // Assertface

                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheUseCaseIsNull()
            {
                // Arrange
                UseCase<RequestA, Response> useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> other = default;
                bool actual;

                // Act
                actual = useCase.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheUseCaseIsDifferentType()
            {
                // Arrange
                UseCase<RequestA, Response> useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestB, Response> other = Substitute.ForPartsOf<UseCase<RequestB, Response>>();
                bool actual;

                // Act
                actual = useCase.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheUseCasesAreDifferentReferences()
            {
                // Arrange
                UseCase<RequestA, Response> useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> other = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCase.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheUseCasesAreTheSame()
            {
                // Arrange
                UseCase<RequestA, Response> useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCase.Equals(useCase);

                // Assertface

                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            [Fact]
            public void GetHashCode_IsInfluencedDifferentByReference_IfTheBaseClassOfAUseCaseIsUsed()
            {
                // Arrange
                UseCase<RequestA, Response> useCase = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> other = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                int hashA, hashB;

                // Act
                hashA = useCase.GetHashCode();
                hashB = other.GetHashCode();

                // Assert
                Assert.NotEqual(hashA, hashB);
            }
        }

        public class TheOperators
        {
            [Fact]
            public void EqualEqual_ReturnsFalse_IfLeftIsNull()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = default;
                UseCase<RequestA, Response> useCaseB = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCaseA == useCaseB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfRightIsNull()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> useCaseB = default;
                bool actual;

                // Act
                actual = useCaseA == useCaseB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfTheyAreNotEqual()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> useCaseB = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCaseA == useCaseB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfTheyAreEqual()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> useCaseB = useCaseA;
                bool actual;

                // Act
                actual = useCaseA == useCaseB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfLeftIsNull()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = default;
                UseCase<RequestA, Response> useCaseB = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCaseA != useCaseB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfRightIsNull()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> useCaseB = default;
                bool actual;

                // Act
                actual = useCaseA != useCaseB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfTheyAreNotEqual()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> useCaseB = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                bool actual;

                // Act
                actual = useCaseA != useCaseB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfTheyAreEqual()
            {
                // Arrange
                UseCase<RequestA, Response> useCaseA = Substitute.ForPartsOf<UseCase<RequestA, Response>>();
                UseCase<RequestA, Response> useCaseB = useCaseA;
                bool actual;

                // Act
                actual = useCaseA != useCaseB;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

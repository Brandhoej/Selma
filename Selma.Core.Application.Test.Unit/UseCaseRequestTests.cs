using NSubstitute;
using Xunit;

namespace Selma.Core.Application.Test.Unit
{
    public class UseCaseRequestTests
    {
        /*
         * int GetHashCode()
         * bool operator !=(UseCaseRequest<TResponse> left, IUseCaseRequest<TResponse> right)
         * bool operator ==(UseCaseRequest<TResponse> left, IUseCaseRequest<TResponse> right)
         */
        public class ResponseA { }
        public class ResponseB { }

        public class TheEqualsMethod
        {

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectIsNull()
            {
                // Arrange
                object useCaseRequest = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                object other = default;
                bool actual;

                // Act
                actual = useCaseRequest.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectIsDifferentType()
            {
                // Arrange
                object useCaseRequest = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                object other = 123;
                bool actual;

                // Act
                actual = useCaseRequest.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheObjectsAreDifferentReferences()
            {
                // Arrange
                object useCaseRequest = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                object other = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                bool actual;

                // Act
                actual = useCaseRequest.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheObjectsAreTheSame()
            {
                // Arrange
                object useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                object useCaseRequestB = useCaseRequestA;
                bool actual;

                // Act
                actual = useCaseRequestA.Equals(useCaseRequestB);

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheUseCaseRequestIsNull()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequest = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> other = default;
                bool actual;

                // Act
                actual = useCaseRequest.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheCaseRequestIsDifferentType()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequest = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseB> other = Substitute.ForPartsOf<UseCaseRequest<ResponseB>>();
                bool actual;

                // Act
                actual = useCaseRequest.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsFalse_WhenTheCaseRequestsAreDifferentReferences()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequest = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> other = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                bool actual;

                // Act
                actual = useCaseRequest.Equals(other);

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void Equals_ReturnsTrue_WhenTheCaseRequestsAreTheSame()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequest = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                bool actual;

                // Act
                actual = useCaseRequest.Equals(useCaseRequest);

                // Assert
                Assert.True(actual);
            }
        }

        public class TheGetHashCodeMethod
        {
            [Fact]
            public void GetHashCode_IsInfluencedDifferentByReference_IfTheBaseClassOfAUseCaseRequestIsUsed()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseB> useCaseRequestB = Substitute.ForPartsOf<UseCaseRequest<ResponseB>>();
                int hashA, hashB;

                // Act
                hashA = useCaseRequestA.GetHashCode();
                hashB = useCaseRequestB.GetHashCode();

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
                UseCaseRequest<ResponseA> useCaseRequestA = default;
                UseCaseRequest<ResponseA> useCaseRequestB = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                bool actual;

                // Act
                actual = useCaseRequestA == useCaseRequestB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfRightIsNull()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> useCaseRequestB = default;
                bool actual;

                // Act
                actual = useCaseRequestA == useCaseRequestB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsFalse_IfTheyAreNotEqual()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> useCaseRequestB = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                bool actual;

                // Act
                actual = useCaseRequestA == useCaseRequestB;

                // Assert
                Assert.False(actual);
            }

            [Fact]
            public void EqualEqual_ReturnsTrue_IfTheyAreEqual()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> useCaseRequestB = useCaseRequestA;
                bool actual;

                // Act
                actual = useCaseRequestA == useCaseRequestB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfLeftIsNull()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = default;
                UseCaseRequest<ResponseA> useCaseRequestB = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                bool actual;

                // Act
                actual = useCaseRequestA != useCaseRequestB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfRightIsNull()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> useCaseRequestB = default;
                bool actual;

                // Act
                actual = useCaseRequestA != useCaseRequestB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsTrue_IfTheyAreNotEqual()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> useCaseRequestB = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                bool actual;

                // Act
                actual = useCaseRequestA != useCaseRequestB;

                // Assert
                Assert.True(actual);
            }

            [Fact]
            public void BangEqual_ReturnsFalse_IfTheyAreEqual()
            {
                // Arrange
                UseCaseRequest<ResponseA> useCaseRequestA = Substitute.ForPartsOf<UseCaseRequest<ResponseA>>();
                UseCaseRequest<ResponseA> useCaseRequestB = useCaseRequestA;
                bool actual;

                // Act
                actual = useCaseRequestA != useCaseRequestB;

                // Assert
                Assert.False(actual);
            }
        }
    }
}

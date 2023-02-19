using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Application.Bookoperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenBookIdLessThanOrEqualZero_ValidationShouldReturnError(int bookId)
        {
            // arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            // act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenBookIdGreaterThanZero_ValidationShouldNotBeReturnError()
        {
            // arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 2;

            // act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}


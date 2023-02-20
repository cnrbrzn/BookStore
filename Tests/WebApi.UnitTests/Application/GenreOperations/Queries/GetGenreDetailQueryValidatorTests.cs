using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenGenreIdLessThanOrEqualZero_ValidationShouldBeReturnError(int genreId)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = genreId;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreIdGreaterThanZero_ValidationShouldNotBeReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = 1;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}
using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context; 
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 99;

            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");
            
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 2;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(g => g.Id == 2);
            genre.Should().BeNull();
        }
    }
}
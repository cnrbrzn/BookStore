using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 99;
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;

            UpdateGenreModel model = new UpdateGenreModel{ Name="Updated Name", IsActive=false};
            command.Model = model;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var updatedGenre = _context.Genres.SingleOrDefault(g=> g.Id == command.GenreId);
            updatedGenre.Should().NotBeNull();
            updatedGenre.Name.Should().Be(model.Name);
        }
    }
}
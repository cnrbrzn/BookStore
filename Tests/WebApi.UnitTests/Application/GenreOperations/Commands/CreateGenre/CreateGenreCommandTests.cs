using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre(){
                Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){Name = genre.Name};

            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut.");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel(){Name="Test"};
            command.Model = model;

            FluentActions.Invoking(()=> command.Handle())
            .Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
            genre.Should().NotBeNull();
        }
    }

}
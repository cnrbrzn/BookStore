using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 999;
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);command.BookId = 1;

            UpdateBookModel model = new UpdateBookModel { Title = "Updated Title", GenreId = 2, AuthorId = 2 };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var updatedBook = _context.Books.SingleOrDefault(b => b.Id == 1);
            updatedBook.Should().NotBeNull();
            updatedBook.Title.Should().Be(model.Title);
            updatedBook.GenreId.Should().Be(model.GenreId);
            updatedBook.AuthorId.Should().Be(model.AuthorId);
        }
    }
}
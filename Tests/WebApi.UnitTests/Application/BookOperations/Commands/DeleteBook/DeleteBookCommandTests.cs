using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.DeleteBook.DeleteBookCommand;

namespace Application.Bookoperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 999;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(b => b.Id == 1);
            book.Should().BeNull();
        }
    }
}
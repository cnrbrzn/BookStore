using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.AuthorOperations.Commands.DeleteAuthor.DeleteAuthorCommand;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
        {

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 9;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı!");
        }
        [Fact]
        public void WhenGivenAuthorHasABookInPublication_InvalidOperationException_ShouldBeReturn()
        {

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarın yayında bir kitabı mevcut, önce kitabı silmelisiniz!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 3;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
            author.Should().BeNull();
        }
    }
}
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

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenBookIdLessThanOrEqualZero_ValidationShouldReturnError(int bookId)
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            // act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenBookIdGreaterThanZero_ValidationShouldNotReturnError()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 12;

            // act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
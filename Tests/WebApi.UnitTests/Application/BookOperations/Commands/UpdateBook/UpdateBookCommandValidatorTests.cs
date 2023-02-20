using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenBookIdIsInvalid_Validator_ShouldHaveError(int bookId)
        {
            // arrange
            UpdateBookModel model = new UpdateBookModel{ Title = "Test Book", GenreId=3, AuthorId=3};
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = model;
            command.BookId = bookId;

            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("", 0, 0)]
        [InlineData(null, 0, 0)]
        [InlineData("x", 1, 1)]
        [InlineData("123", 2, 2)]
        public void WhenModelIsInvalid_Validator_ShouldHaveError(string title, int genreId, int authorId)
        {
            // arrange
            UpdateBookModel model = new UpdateBookModel();
            model.Title = title;
            model.GenreId = genreId;
            model.AuthorId = authorId;
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = model;

            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Title", 1, 1)]
        [InlineData("Long Title", 2, 2)]
        public void WhenInputsAreValid_Validator_ShouldNotHaveError(string title, int genreId, int authorId)
        {
            // arrange
            UpdateBookModel model = new UpdateBookModel();
            model.Title = title;
            model.GenreId = genreId;
            model.AuthorId = authorId;
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 2;
            command.Model = model;

            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
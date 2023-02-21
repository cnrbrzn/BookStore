using System;
using FluentAssertions;
using TestSetup;
using Xunit;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using AutoMapper;
using static WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using System.Linq;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenAuthorIdIsInvalid_Validator_ShouldHaveError(int authorId)
        {
            // arrange
            UpdateAuthorModel model = new UpdateAuthorModel{ Name = "Test Name", Surname = "Test Surname" };
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = model;
            command.AuthorId = authorId;

            // act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData("", "")]
        [InlineData("sa", "ds")]
        [InlineData("sa", "")]
        [InlineData("", "ds")]
        public void WhenModelIsInvalid_Validator_ShouldHaveError(string name, string surname)
        {
            // arrange
            UpdateAuthorModel model = new UpdateAuthorModel();
            model.Name = name;
            model.Surname = surname;
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = model;

            // act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Name", "Surname")]
        public void WhenInputsAreValid_Validator_ShouldNotHaveError(string name, string surname)
        {
            // arrange
            UpdateAuthorModel model = new UpdateAuthorModel();
            model.Name = name;
            model.Surname = surname;
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 2;
            command.Model = model;

            // act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
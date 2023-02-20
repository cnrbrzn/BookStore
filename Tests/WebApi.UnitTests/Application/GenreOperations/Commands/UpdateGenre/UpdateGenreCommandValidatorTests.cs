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
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenGenreIdIsInvalid_Validator_ShouldHaveError(int genreId)
        {
            UpdateGenreModel model = new UpdateGenreModel { Name = "Test Genre", IsActive=false};
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = model;
            command.GenreId = genreId;
            
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("n",1,true)]
        [InlineData("12",2,true)]
        public void WhenModelIsInvalid_Validator_ShouldHaveError(string name, int genreId, bool isActive)
        {
            UpdateGenreModel model = new UpdateGenreModel();
            model.Name = name;
            model.IsActive = isActive;
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = model;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Test",1,true)]
        [InlineData("Test test",2,true)]
        public void WhenInputsAreValid_Validator_ShouldNotHaveError(string name, int genreId, bool isActive)
        {
            UpdateGenreModel model = new UpdateGenreModel();
            model.Name = name;
            model.IsActive = isActive;
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = model;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
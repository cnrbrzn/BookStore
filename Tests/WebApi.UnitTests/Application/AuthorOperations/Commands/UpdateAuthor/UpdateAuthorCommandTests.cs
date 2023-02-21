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
    public class UpdateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 9;
            FluentActions.Invoking(()=> command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Yazar Bulunamadı!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            // arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;

            UpdateAuthorModel model = new UpdateAuthorModel { Name = "Updated Name", Surname = "Updated Surname"};
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var updatedAuthor = _context.Authors.SingleOrDefault(b => b.Id == command.AuthorId);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.Name.Should().Be(model.Name);
            updatedAuthor.Surname.Should().Be(model.Surname);
        }
    }
}
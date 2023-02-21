using System;
using FluentAssertions;
using TestSetup;
using Xunit;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Entities;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using System.Linq;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorNameAndSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author(){
                Name = "Test_Name",
                Surname = "Test_Surname",
                Birthday = new DateTime(1983,03,15)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel(){Name=author.Name, Surname=author.Surname,Birthday = author.Birthday};

            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut.");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            CreateAuthorModel model = new CreateAuthorModel(){Name="Test_Name",Surname="Test_Surname",Birthday= DateTime.Now.Date.AddYears(-50)};
            command.Model = model;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && author.Surname == model.Surname);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.Birthday.Should().Be(model.Birthday);
        }
    }
}
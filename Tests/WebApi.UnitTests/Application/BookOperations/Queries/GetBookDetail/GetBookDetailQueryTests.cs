using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 11;

            // assert
            query.Invoking(x => x.Handle())
                 .Should().Throw<InvalidOperationException>()
                 .And.Message.Should().Be("Kitap Bulunamadı!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
        {
            // arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 1;

            // act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(b=> b.Id == query.BookId);
            book.Should().NotBeNull();
        }
    }
}
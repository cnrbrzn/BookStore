using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = 12;

            query.Invoking(x=> x.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap Türü Bulunamadı!");

            var genre = _context.Genres.SingleOrDefault(g=> g.Id == query.GenreId);
            genre.Should().BeNull();
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(g=> g.Id == query.GenreId);
            genre.Should().NotBeNull();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var query = _context.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(query is null)
                throw new InvalidOperationException("Yazar bulunamadı");
            AuthorDetailViewModel author = _mapper.Map<AuthorDetailViewModel>(query);
            return author;
        }
    }
    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}
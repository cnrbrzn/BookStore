using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var query = _context.Authors.OrderBy(x=> x.Id).ToList<Author>();
            List<AuthorsViewModel> authors = _mapper.Map<List<AuthorsViewModel>>(query);
            return authors;
        }
    }
    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}
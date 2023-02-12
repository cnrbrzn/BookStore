using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model {get; set;}
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
             var author = _dbContext.Authors.SingleOrDefault(author=> author.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!");
            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
            _dbContext.SaveChanges();
        }

        public class UpdateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(author => author.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı!");
            if(_dbContext.Books.Any(x=>x.AuthorId == AuthorId))
                throw new InvalidOperationException("Yazarın yayında bir kitabı mevcut, önce kitabı silmelisiniz!");
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}

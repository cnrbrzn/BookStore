using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model {get; set;}
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id)
        {
             var book = _dbContext.Books.SingleOrDefault(book=> book.Id == id);
            if(book is null)
                throw new InvalidOperationException("Böyle bir kitap bulunamadı");
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
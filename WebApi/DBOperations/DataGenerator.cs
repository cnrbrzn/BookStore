using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }
                );
                context.Authors.AddRange(
                    new Author{
                        Name = "Agatha",
                        Surname = "Christie",
                        Birthday = new DateTime(1976, 01, 12)
                    },
                    new Author{
                        Name = "William",
                        Surname = "Shakespeare",
                        Birthday = new DateTime(1564, 04, 26)
                    },
                    new Author{
                        Name = "Stephen",
                        Surname = "King",
                        Birthday = new DateTime(1947, 09, 21)
                    }
                );
                context.Books.AddRange(
                new Book
                {
                    //Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1, 
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12),
                    AuthorId = 1,
                },
                new Book
                {
                    //Id = 2,
                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23),
                    AuthorId = 2,
                },
                new Book
                {
                    //Id = 3,
                    Title = "Dune",
                    GenreId = 2, 
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21),
                    AuthorId = 2
                }
                );

                context.SaveChanges();
            }
        }
    }
}
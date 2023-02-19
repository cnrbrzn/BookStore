using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author{ Name = "Agatha", Surname = "Christie", Birthday = new DateTime(1976, 01, 12)},
                    new Author{ Name = "William", Surname = "Shakespeare", Birthday = new DateTime(1564, 04, 26)},
                    new Author{ Name = "Stephen", Surname = "King", Birthday = new DateTime(1947, 09, 21)});
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using LibraryTestApp.Models;

namespace LibraryTestApp.Data
{
    public static class MockData
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User()
                {
                    Id = 1,
                    Name = "Jae Doe",
                    Email = "jaedoe@mail.com"
                }
            });
        }

        public static void SeedBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(new Book[]
            {
                new Book()
                {
                    Id = 1,
                    Title = "Bookname",
                    Author = "John Doe",
                    Genre = "Fantasy",
                    Available = true
                }
            });
        }
    }
}

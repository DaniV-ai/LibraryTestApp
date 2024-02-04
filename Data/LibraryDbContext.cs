using Microsoft.EntityFrameworkCore;
using LibraryTestApp.Models;

namespace LibraryTestApp.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() { }

        public LibraryDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(@"Server=localhost:5432;Database=LibraryTestDB;User ID=user");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(e => e.Books)
                                        .WithMany(e => e.Users);

            modelBuilder.Entity<Book>().HasMany(e => e.Users)
                                        .WithMany(e => e.Books);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookUser> BooksUsers { get; set; }

    }
}

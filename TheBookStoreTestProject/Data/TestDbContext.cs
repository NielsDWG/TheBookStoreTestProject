using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheBookStoreTestProject.Data.Models;

namespace TheBookStoreTestProject.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Author> Batch { get; set; }
        public DbSet<Book> BatchDocument { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().ToTable("Authors")
            .HasData(SeedData.Authors());

            modelBuilder.Entity<Book>().ToTable("Books")
            .HasData(SeedData.Books());
        }
    }
}

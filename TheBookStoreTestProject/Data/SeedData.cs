using TheBookStoreTestProject.Data.Models;

namespace TheBookStoreTestProject.Data
{
    public class SeedData
    {
        public static Book[] Books()
        {
            return new Book[]
            {
                new Book{ Id = 1, AuthorId = 1, ISBN = "12345678910111", Title = "Book 1" },
                new Book{ Id = 2, AuthorId = 1, ISBN = "12345678910112", Title = "Book 2" },
                new Book{ Id = 3, AuthorId = 1, ISBN = "12345678910113", Title = "Book 3" },
                new Book{ Id = 4, AuthorId = 2, ISBN = "12345678910114", Title = "Book 4" },
                new Book{ Id = 5, AuthorId = 2, ISBN = "12345678910115", Title = "Book 5" }
            };
        }

        public static Author[] Authors()
        {
            return new Author[]
            {
                new Author{ Id = 1, Firstname = "Author1", Lastname = "Author1" },
                new Author{ Id = 2, Firstname = "Author2", Lastname = "Author2" }
            };
        }
    }
}

using System.Linq;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;

namespace TheBookStoreTestProject.Logic.Helper
{
    public static class CustomMapper
    {
        public static IQueryable<AuthorDTO> ProjectTo(IQueryable<Author> source, int depth)
        {
            return source?.Select(item => item.ToDto(depth));
        }

        //[Computed]
        public static AuthorDTO ToDto(this Author author, int depth)
        {
            return depth < 0 ? null : new AuthorDTO
            {
                Firstname = author.Firstname,
                Lastname = author.Lastname,
                Id = author.Id,
                Books = author.Books?.Select(book => book.ToDto(depth - 1))
            };
        }

        //[Computed]
        public static BookDTO ToDto(this Book book, int depth)
        {
            return depth < 0 ? null : new BookDTO
            {
                AuthorId = book.AuthorId,
                Author = book.Author?.ToDto(depth - 1),
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title
            };
        }        
    }
}

using DelegateDecompiler;
using System.Linq;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;

namespace TheBookStoreTestProject.Logic.Helper
{
    public static class CustomMapper
    {
        public static IQueryable<AuthorDTO> ProjectTo(IQueryable<Author> source)
        {
            return source?.Select(item => item.ToDto());
        }

        [Computed]
        public static AuthorDTO ToDto(this Author author)
        {
            return new AuthorDTO
            {
                Firstname = author.Firstname,
                Lastname = author.Lastname,
                Id = author.Id,
                Books = author.Books.Select(book => book.ToDto())
            };
        }      

        [Computed]
        public static BookDTO ToDto(this Book book)
        {
            return new BookDTO
            {
                AuthorId = book.AuthorId,
                //Author = book.Author.ToDto(),
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title
            };
        }
    }
}

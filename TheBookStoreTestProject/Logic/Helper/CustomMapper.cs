using LinqToDB;
using System;
using System.Linq;
using System.Linq.Expressions;
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

        [ExpressionMethod(nameof(ToDtoAuthor))]
        public static AuthorDTO ToDto(this Author author)
        {
            _toDtoAuthor ??= ToDtoAuthor().Compile();
            return _toDtoAuthor(author);
        }

        private static Func<Author, AuthorDTO> _toDtoAuthor;

        private static Expression<Func<Author, AuthorDTO>> ToDtoAuthor()
        {
            return author => new AuthorDTO
            {
                Firstname = author.Firstname,
                Lastname = author.Lastname,
                Id = author.Id,
                Books = author.Books.Select(book => book.ToDto())
            };
        }

        [ExpressionMethod(nameof(ToDtoBook))]
        public static BookDTO ToDto(this Book book)
        {
            _toDtoBook ??= ToDtoBook().Compile();
            return _toDtoBook(book);
        }

        private static Func<Book, BookDTO> _toDtoBook;

        private static Expression<Func<Book, BookDTO>> ToDtoBook()
        {
            return book => new BookDTO
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

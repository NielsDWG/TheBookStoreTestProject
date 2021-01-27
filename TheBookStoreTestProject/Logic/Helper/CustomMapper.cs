using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;

namespace TheBookStoreTestProject.Logic.Helper
{
    public class CustomMapper
    {
        public static IQueryable<AuthorDTO> ProjectTo(IQueryable<Author> source)
        {
            return source?.Select(ProjectToAuthorDto());
        }        

        public static AuthorDTO Map(Author author)
        {
            return author == null ? null : ProjectToAuthorDto().Compile()(author);
        }
        public static BookDTO Map(Book book)
        {
            return book == null ? null : ProjectToBookDto().Compile()(book);
        }

        private static List<BookDTO> ProjectTo(IEnumerable<Book> source)
        {
            return source?.Select(item => Map(item)).ToList();
        }

        private static Expression<Func<Author, AuthorDTO>> ProjectToAuthorDto()
        {
            return author => new AuthorDTO
            {
                Firstname = author.Firstname,
                Lastname = author.Lastname,
                Id = author.Id,
                Books = ProjectTo(author.Books)
            };
        }

        private static Expression<Func<Book, BookDTO>> ProjectToBookDto()
        {
            return book => new BookDTO
            {
                AuthorId = book.AuthorId,
                Author = Map(book.Author),
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title
            };
        }
    }
}

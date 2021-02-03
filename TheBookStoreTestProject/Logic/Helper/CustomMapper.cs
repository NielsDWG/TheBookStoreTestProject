using System;
using System.Collections.Generic;
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
            return source?.Select(ProjectToAuthorDto());
        }
       
        public static AuthorDTO ToDto(this Author author)
        {
            return ProjectToAuthorDto().Compile().Invoke(author);
        }

        private static Expression<Func<Author, AuthorDTO>> ProjectToAuthorDto()
        {
            return author => new AuthorDTO
            {
                Firstname = author.Firstname,
                Lastname = author.Lastname,
                Id = author.Id,
                Books = author.Books.Select(book => book.ToDto())
            };
        }





        public static IQueryable<BookDTO> ProjectTo(IQueryable<Book> source)
        {
            return source?.Select(ProjectToBookDto());
        }

        public static BookDTO ToDto(this Book book)
        {
            return ProjectToBookDto().Compile().Invoke(book);
        }

        private static Expression<Func<Book, BookDTO>> ProjectToBookDto()
        {
            return book => new BookDTO
            {
                AuthorId = book.AuthorId,
                Author = book.Author.ToDto(),
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title
            };
        }
    }
}

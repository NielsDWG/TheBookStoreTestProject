using DelegateDecompiler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;

namespace TheBookStoreTestProject.Logic.Helper
{
    public static class CustomMapper
    {
        public static IEnumerable<AuthorDTO> ProjectTo(IEnumerable<Author> source, int depth)
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
        public static ODataQueryOptions<Author> ToDto(this ODataQueryOptions<AuthorDTO> options)
        {
            return new ODataQueryOptions<Author>(options.Context, options.Request)
            {
                
            };
        }
    }

    public static class ODataBuilder
    {
        public static ODataQueryOptions RecreateQueryOptions<T>(ODataQueryOptions queryOptions, HttpRequest request)
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.AddEntityType(typeof(T));

            ODataQueryContext queryContext = new ODataQueryContext(modelBuilder.GetEdmModel(), typeof(T), queryOptions.Request.ODataFeature().Path);
            
            return new ODataQueryOptions(queryContext, request);
        }
    }
}

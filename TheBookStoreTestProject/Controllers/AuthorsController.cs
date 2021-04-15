using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData.Query;
using System.Linq;
using TheBookStoreTestProject.Data;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;
using TheBookStoreTestProject.Logic.Helper;
using Microsoft.AspNet.OData;

namespace TheBookStoreTestProject.Controllers
{
    public class AuthorsController : ODataController
    {
        private readonly TestDbContext dbContext;

        public AuthorsController(TestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        public ActionResult Get(ODataQueryOptions<AuthorDTO> options)
        {
            // Get data
            IQueryable<Author> authors = dbContext.Set<Author>();//.Include(a => a.Books);//.ThenInclude(b => b.Author);

            // Apply OData query options
            IQueryable<Author> resultList = ODataHelper.ApplyODataQueryOptions(authors, options, Request);

            // Convert to DTO
            IQueryable<AuthorDTO> result = CustomMapper.ProjectTo(resultList.AsQueryable(), 2);

            // Apply OData select and expand
            return Ok(ODataHelper.ApplyODataSelectExpand(result, options));
        }
    }
}

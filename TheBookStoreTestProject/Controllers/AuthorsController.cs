using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using TheBookStoreTestProject.Data;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;
using TheBookStoreTestProject.Logic;
using TheBookStoreTestProject.Logic.Helper;

namespace TheBookStoreTestProject.Controllers
{
    public class AuthorsController : ODataController
    {
        private readonly TestDbContext dbContext;

        public AuthorsController(TestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [EnableQuery]
        public ActionResult Get()
        {
            // Get data
            IQueryable<Author> authors = dbContext.Set<Author>();

            // Convert to DTO
            IQueryable<AuthorDTO> result = CustomMapper.ProjectTo(authors);

            // This produces the desired result, but does not apply
            // the odata filter to the query as it is not an IQuerable anymore
            //
            //return Ok(result.ToList());

            return Ok(result);
        }
    }
}

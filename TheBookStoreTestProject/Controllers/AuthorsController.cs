using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;
using TheBookStoreTestProject.Data;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;
using TheBookStoreTestProject.Logic;
using TheBookStoreTestProject.Logic.Helper;

namespace TheBookStoreTestProject.Controllers
{
    public class AuthorsController : Controller
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

            // Return result
            return Ok(result);
        }       
    }
}

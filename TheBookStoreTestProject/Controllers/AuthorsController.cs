using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
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
            // This works correctly
            //AuthorDTO author = dbContext.Set<Author>().Include(a => a.Books).ThenInclude(b => b.Author).FirstOrDefault(a => a.Id == 1).ToDto();
            //BookDTO book = dbContext.Set<Book>().Include(b => b.Author).FirstOrDefault(b => b.Id == 1).ToDto();

            // Get data
            IQueryable<Author> authors = dbContext.Set<Author>().Include(a => a.Books).ThenInclude(b => b.Author);

            // Convert to DTO
            IQueryable<AuthorDTO> result = CustomMapper.ProjectTo(authors);

            return Ok(result);
        }
    }
}

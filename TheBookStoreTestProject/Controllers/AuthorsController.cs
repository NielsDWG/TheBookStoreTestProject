using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheBookStoreTestProject.Data;
using TheBookStoreTestProject.Data.Models;
using TheBookStoreTestProject.DTO;
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

        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        public ActionResult Get()
        {
            // Get data
            IQueryable<Author> authors = dbContext.Set<Author>().Include(a => a.Books);

            // Convert to DTO
            IQueryable<AuthorDTO> result = CustomMapper.ProjectTo(authors);

            return Ok(result.DecompileAsync());
        }
    }
}

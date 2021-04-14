using AutoMapper;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly IMapper mapper;

        public AuthorsController(TestDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;

            this.mapper = mapper;
        }

        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        public ActionResult Get(ODataQueryOptions<AuthorDTO> options)
        {
            // Get data
            IQueryable<Author> authors = dbContext.Set<Author>();//.Include(a => a.Books);//.ThenInclude(b => b.Author);

            ODataQueryOptions mappedOptions = ODataBuilder.RecreateQueryOptions<Author>(options, Request);

            IQueryable odataResult = mappedOptions.ApplyTo(authors);

            ICollection<Author> resultList;

            if (mappedOptions.SelectExpand == null)
            {
                resultList = (odataResult as IQueryable<Author>).ToList();
            }
            else
            {
                resultList = new List<Author>();
                foreach (var item in odataResult)
                {                    
                    if (item.GetType().Name == "SelectAllAndExpand`1")
                    {
                        var entityProperty = item.GetType().GetProperty("Instance");
                        resultList.Add((Author)entityProperty.GetValue(item));
                    }
                    else if (item is Author)
                    {
                        resultList.Add((Author)item);
                    }
                }
            }

            // Convert to DTO
            IEnumerable<AuthorDTO> result = CustomMapper.ProjectTo(resultList, 2);

            return Ok(result);
        }
    }
}

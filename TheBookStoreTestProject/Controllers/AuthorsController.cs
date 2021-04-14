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

        //[EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        public ActionResult Get(ODataQueryOptions<AuthorDTO> options)
        {
            // Get data
            IQueryable<Author> authors = dbContext.Set<Author>();//.Include(a => a.Books);//.ThenInclude(b => b.Author);

            // Apply OData query options
            var resultList = ODataHelper.ApplyODataQueryOptions(authors, options, Request);

            // Convert to DTO
            IQueryable<AuthorDTO> result = CustomMapper.ProjectTo(resultList.AsQueryable(), 2);

            // Apply OData select and expand
            return Ok(ODataHelper.ApplyODataSelectExpand(result, options));
        }
    }
}

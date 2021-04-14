using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.ModelBuilder;
using System.Collections.Generic;
using System.Linq;

namespace TheBookStoreTestProject.Logic.Helper
{
    public static class ODataHelper
    {
        /// <summary>
        /// Function to apply OData select and expand
        /// </summary>
        /// <typeparam name="T">A dto</typeparam>
        /// <param name="source">Resultset after mapping</param>
        /// <param name="originalOptions">The request ODataQueryOptions</param>
        /// <returns>IQueryable result</returns>
        public static IQueryable ApplyODataSelectExpand<T>(IQueryable<T> source, ODataQueryOptions originalOptions)
        {
            var ignoredOptions = AllowedQueryOptions.Filter
                | AllowedQueryOptions.OrderBy 
                | AllowedQueryOptions.Top
                | AllowedQueryOptions.Skip 
                | AllowedQueryOptions.Format 
                | AllowedQueryOptions.SkipToken 
                | AllowedQueryOptions.DeltaToken 
                | AllowedQueryOptions.Apply;

            return originalOptions.ApplyTo(source, ignoredOptions);
        }

        /// <summary>
        /// Function to apply ODataQuery options on an IQueryable with an other model
        /// </summary>
        /// <typeparam name="T">The database model equivalent of the Dto</typeparam>
        /// <param name="source">Database table</param>
        /// <param name="originalOptions">The ODataOptions from the request</param>
        /// <param name="request">The request</param>
        /// <returns>Resultset of T with the options applied</returns>
        public static IQueryable<T> ApplyODataQueryOptions<T>(IQueryable<T> source, ODataQueryOptions originalOptions, HttpRequest request)
        {
            // Create new ODataqueryOptions with the database model
            ODataQueryOptions mappedOptions = ODataHelper.RecreateQueryOptions<T>(originalOptions, request);

            // Apply options to source
            IQueryable odataResult = mappedOptions.ApplyTo(source, new ODataQuerySettings(), AllowedQueryOptions.Select);

            ICollection<T> result;

            // If there is no select or expand
            if (mappedOptions.SelectExpand == null)
            {
                // Cast the result
                result = (odataResult as IQueryable<T>).ToList();
            }
            else
            {
                // Loop over the IQueryable to get the items
                result = new List<T>();
                foreach (var item in odataResult)
                {
                    // Check if the result is the expected result
                    if (item.GetType().Name == "SelectAllAndExpand`1")
                    {
                        var entityProperty = item.GetType().GetProperty("Instance");
                        result.Add((T)entityProperty.GetValue(item));
                    }
                    else if (item is T t)
                    {
                        // TODO: validate if this 'else if' is ever executed
                        result.Add(t);
                    }
                }
            }

            return result.AsQueryable();
        }


        private static ODataQueryOptions RecreateQueryOptions<T>(ODataQueryOptions queryOptions, HttpRequest request)
        {
            // Build new 'fake' model
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.AddEntityType(typeof(T));

            // Create new context with new model
            ODataQueryContext queryContext = new ODataQueryContext(modelBuilder.GetEdmModel(), typeof(T), queryOptions.Request.ODataFeature().Path);

            // Return new options
            return new ODataQueryOptions(queryContext, request);
        }
    }
}

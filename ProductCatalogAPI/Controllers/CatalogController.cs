using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        //naming convention when it's a private variable to use an underscore
        private readonly CatalogContext _catalogContext;

        //constructor
        public CatalogController(CatalogContext catalogContext)
        {
            //the constructor will now know what database it is going to talk to and then set the
            //private variable so that everyone in the class has access
            _catalogContext = catalogContext;
        }

        [HttpGet]
        [Route("[action]")] //square brackets for the name of the method I want to access
        public async Task<IActionResult> CatalogTypes()
        {
            //we made this async because we have a dbSet called CatalogTypes
            //convert all the entries into a list from the table and I want
            //to add it in an asynchronous fashion
            var items = await _catalogContext.CatalogTypes.ToListAsync();
            return Ok(items); //will convert to JSON

        }

        [HttpGet]
        [Route("[action]")] //square brackets for the name of the method I want to access
        public async Task<IActionResult> CatalogBrands()
        {
            //we made this async because we have a dbSet called CatalogTypes
            //convert all the entries into a list from the table and I want
            //to add it in an asynchronous fashion
            var items = await _catalogContext.CatalogBrands.ToListAsync();
            return Ok(items); //will convert to JSON

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items(
           [FromQuery] int pageSize = 6,
           [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _catalogContext.CatalogItems
                              .LongCountAsync();
           
            var itemsOnPage = await _catalogContext.CatalogItems
                              .OrderBy(c => c.Name) 
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();

            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);

           

            return Ok(itemsOnPage);
        }

        private List<CatalogItem> ChangeUrlPlaceHolder(List<CatalogItem> items)
        {
            items.ForEach(
                x => x.PictureUrl =
                x.PictureUrl
                .Replace("http://externalcatalogbaseurltobereplaced",
                "http://localhost:5030"));
            return items;
        }
    }
}
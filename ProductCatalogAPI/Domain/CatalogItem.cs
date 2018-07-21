using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Domain
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        //foreign key relationship
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        //a virtual relationship doesn't take up physical space, not a 
        // physical column in the table, it's a "fake" relationship
        public virtual CatalogType CatalogType { get; set; }
        public CatalogBrand CatalogBrand { get; set; }



    }
}

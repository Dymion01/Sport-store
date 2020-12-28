using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Models
{
    class FakeProductRepository /* : IProductRepository */
    {
        public IQueryable<Product> Products => new List<Product>  {

            new Product { Name = "Piłka" , Price = 22},
             new Product { Name = "Deska" , Price = 212},
              new Product { Name = "Buty" , Price = 322},
        }.AsQueryable<Product>();

    }
}

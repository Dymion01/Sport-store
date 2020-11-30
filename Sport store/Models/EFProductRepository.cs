using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        public EFProductRepository(AppDbContext ctx)
        {

            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
    }
}

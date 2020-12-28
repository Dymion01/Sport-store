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
        public IQueryable<Category> Categories => context.Category;

        public void SaveProduct(Product product)
        {
            Category EditedCategory;
            if (Categories.Any(c=>c.Name == product.Category.Name)){
                EditedCategory = Categories.FirstOrDefault(c => c.Name == product.Category.Name);
           }
            else
            {
                context.Category.Add(new Category { Name = product.Category.Name });
                context.SaveChanges();
                EditedCategory = Categories.FirstOrDefault(c => c.Name == product.Category.Name);
            }
            if(product.Id == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.FirstOrDefault(p => p.Id == product.Id);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = EditedCategory;
                }
            }
            context.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_store.Models;
using Sport_store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;

        }

        public ViewResult List(string category, int productPage = 1)
           => View(new ProductsListViewModel
           {
               Products = _repository.Products
                .Where(p => String.IsNullOrEmpty(category) || p.Category.Name == category)
                .Include(t => t.Category)
                .OrderBy(p => p.Id)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
               PagingInfo = new PagingInfo
               {
                   CurrentPage = productPage,
                   ItemsPerPage = PageSize,
                   TotalItems = string.IsNullOrEmpty(category) ?
                        _repository.Products.Count(): 
                        _repository.Products.Where(e => e.Category.Name == category).Count()

               },
               CurrentCategory = category


           });
                
    }
}

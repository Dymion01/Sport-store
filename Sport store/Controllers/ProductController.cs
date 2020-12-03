using Microsoft.AspNetCore.Mvc;
using Sport_store.Models;
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

        public ViewResult List(int productPage = 1)
            => View(_repository.Products
                .OrderBy(p => p.Id)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize));
                
    }
}

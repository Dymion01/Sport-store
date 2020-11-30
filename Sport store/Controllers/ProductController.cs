using Microsoft.AspNetCore.Mvc;
using Sport_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Controllers
{
    public class ProductController
    {
        private IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;

        }

        public ViewResult List() => View(_repository.Products);
    }
}

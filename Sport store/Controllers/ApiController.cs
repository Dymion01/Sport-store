using Microsoft.AspNetCore.Mvc;
using Sport_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private EFProductRepository _EFProductRepository;
        private readonly AppDbContext _context;

        public ApiController(AppDbContext context )
        {
            _EFProductRepository = new EFProductRepository(context);
        }

        [HttpGet]
        public IQueryable<Product> Get() => _EFProductRepository.Products;

        [HttpGet("{id}")]
        public Product Get(int id) => _EFProductRepository.Products.FirstOrDefault(x => x.Id == id);

        [HttpPost]
        public Product Post(Product product)
        {
            _EFProductRepository.SaveProduct(product);
            return product;
        }

        [HttpPut]
        public Product Put(Product product)
        {
            _EFProductRepository.SaveProduct(product);
            return product;
        }
        [HttpDelete("id")]
        public void Delete(int id) => _EFProductRepository.DeleteProduct(id);

    }
}

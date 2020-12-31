using Microsoft.AspNetCore.Mvc;
using Sport_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public IEnumerable<Category> categories;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
            categories = repo.Categories.ToArray();
        }
        public ViewResult Index() => View(repository.Products);
        
        public ViewResult Edit(int productId) => View(repository.Products.FirstOrDefault(p => p.Id == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"Zapisano {product.Name}.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create() 
        {
            ViewBag.EditOrCreate = "Dodaj nowy produkt";
           return View("Edit", new Product());
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"Usunięto {deletedProduct.Name}";
            }
            return RedirectToAction("Index");
        }

    }
}

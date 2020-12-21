using Microsoft.AspNetCore.Mvc;
using Sport_store.Models;
using Microsoft.AspNetCore.Http;
using Sport_store.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sport_store.Models.ViewModels;

namespace Sport_store.Controllers
{

    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            }); ;
        }

        public RedirectToActionResult AddToCart(int Id, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {                
                cart.AddItem(product, 1);                
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });

        }
    }
}

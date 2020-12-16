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

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int Id, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveItem(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });

        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}

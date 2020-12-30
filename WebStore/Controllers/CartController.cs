using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infastrature.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Cart()
        {
            return View(_cartService.TransformFromCart());
        }

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction(nameof(Cart));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Cart));
        }
    }
}

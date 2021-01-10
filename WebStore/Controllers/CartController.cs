using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

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
            return View(new OrderDetailViewModel
            {
                CartViewModel = _cartService.TransformFromCart(),
                OrderViewModel = new OrderViewModel()
            });
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

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderViewModel orderViewModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
            {
                // !!!!!!
            }

            var order = await orderService.CreateOrder(User.Identity.Name, _cartService.TransformFromCart(), orderViewModel);

            _cartService.RemoveAll();

            return RedirectToAction(nameof(OrderConfmird), order.Id);
        }


        public IActionResult OrderConfmird(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}

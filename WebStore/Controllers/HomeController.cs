using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller //ControllerBase  для веб апи
    {
        public IActionResult Error404() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Cart() => View();

        public IActionResult CheckOut() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Login() => View();

        public IActionResult ProductDetails() => View();

        public IActionResult Shop() => View();

        public IActionResult Index()
        {
            return View();
            // return Content("Home controller");
        }
    }
}
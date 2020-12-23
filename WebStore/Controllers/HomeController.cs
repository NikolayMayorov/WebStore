using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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



        public IActionResult Index() => View();

        public EmptyResult GetEmty() => new EmptyResult();

        public ApplicationException GetExc(string id)
        {
            throw new ApplicationException(id);
        }

      
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebStore.DomainCore.Entities.Identity;

namespace WebStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Index()
        {
            return View();
        }
    }
}

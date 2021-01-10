using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebStore.DomainCore.Entities.Identity;

namespace WebStore.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    [Authorize(Roles = Role.Administrator)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

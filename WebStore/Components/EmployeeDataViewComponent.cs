using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainCore.Entities.Identity;

namespace WebStore.Components
{
    public class EmployeeDataViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (User.IsInRole(Role.Administrator))
                return View("Admin");
            else
                return View();

        }
    }
}

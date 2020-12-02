using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller //ControllerBase  для веб апи
    {
        private static readonly List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                FirstName = "Ivan",
                SurName = "Ivanov",
                Patronymic = "Ivanovich",
                Age = 54
            },
            new Employee
            {
                Id = 2,
                FirstName = "Sergey",
                SurName = "Petrov",
                Patronymic = "Bogdanovich",
                Age = 28
            },
            new Employee
            {
                Id = 3,
                FirstName = "Boris",
                SurName = "Grozniy",
                Patronymic = "Ivanovich",
                Age = 19
            }
        };

        public IActionResult Index()
        {
            return View();
            // return Content("Home controller");
        }

        public IActionResult Employee()
        {
            return View(model: employees);
        }


        public IActionResult EmployeeData(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp is null)
                return NotFound();
            return View(model: emp);
        }
    }
}
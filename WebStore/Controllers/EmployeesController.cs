using Microsoft.AspNetCore.Mvc;
using WebStore.Infastrature.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    // [Route("users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData employeesData;

        public EmployeesController(IEmployeesData employeesData)
        {
            this.employeesData = employeesData;
        }

        //   [Route("emp/")]
        public IActionResult Index()
        {
            // return View(TestData.Employees);
            return View(employeesData.GetAll());
        }

        // [Route("emp/{id}")]
        public IActionResult EmployeeData(int id)
        {
            // var emp = TestData.Employees.FirstOrDefault(e => e.Id == id);
            var emp = employeesData.GetById(id: id);
            if (emp is null)
                return NotFound();
            return View(model: emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            if (employee.Id == 0)
            {
                employeesData.Add(employee);

            }
            else
            {
                employeesData.Edit(employee.Id, employee);
            }
            employeesData.SaveChanges();
          
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new Employee());

            if (id < 0)
                return BadRequest();

            var emp = employeesData.GetById((int) id);

            if (emp is null)
                return NotFound();

            return View(emp);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View();
            employeesData.Add(employee);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var emp = employeesData.GetById(id);

            if (emp is null)
                return NotFound();

            return View(emp);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            employeesData.Delete(id);
            employeesData.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
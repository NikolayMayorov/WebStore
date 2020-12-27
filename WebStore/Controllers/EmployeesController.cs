using Microsoft.AspNetCore.Mvc;
using WebStore.DomainCore.Entities;
using WebStore.Infastrature.Interfaces;


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
            if (employee.Age > 100) 
                ModelState.AddModelError(key: string.Empty, "Слишком стар");

            if (!ModelState.IsValid)
                return View(model: employee);

            if (employee.Id == 0)
                employeesData.Add(emp: employee);
            else
                employeesData.Edit(id: employee.Id, emp: employee);
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

            return View(model: emp);
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
            employeesData.Add(emp: employee);
            employeesData.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var emp = employeesData.GetById(id: id);

            if (emp is null)
                return NotFound();

            return View(model: emp);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            employeesData.Delete(id: id);
            employeesData.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
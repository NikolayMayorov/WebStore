using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infastrature.Interfaces;
using WebStore.Models;

namespace WebStore.Infastrature.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        public IEnumerable<Employee> GetAll()
        {
            return TestData.Employees;
        }

        public Employee GetById(int id)
        {
            return TestData.Employees.FirstOrDefault(emp => emp.Id == id);
        }

        public void Add(Employee emp)
        {
            if (emp is null)
                throw new ArgumentNullException(nameof(emp));
            emp.Id = TestData.Employees.Max(e => e.Id) + 1;
            TestData.Employees.Add(emp);
        }

        public void Edit(int id, Employee emp)
        {
            if (emp is null)
                throw new ArgumentNullException(nameof(emp));
            var db_emp = GetById(id);
            if (db_emp is null)
                return;
            db_emp.SurName = emp.SurName;
            db_emp.Patronymic = emp.Patronymic;
            db_emp.Age = emp.Age;
            db_emp.FirstName = emp.FirstName;
        }

        public bool Delete(int id)
        {
            var db_emp = GetById(id);
            if (db_emp is null)
                return false;
            var result = TestData.Employees.Remove(db_emp);
            return result;
        }

        public void SaveChanges()
        {
        }
    }
}

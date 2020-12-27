using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainCore.Entities;
using WebStore.Infastrature.Interfaces;

namespace WebStore.Infastrature.Services.InSQL
{
    public class InSQLEmployeeData : IEmployeesData
    {
        private readonly WebStoreDB _webStoreDb;

        public InSQLEmployeeData(WebStoreDB webStoreDB)
        {
            _webStoreDb = webStoreDB;
        }


        public IEnumerable<Employee> GetAll()
        {
            return _webStoreDb.Employee.AsEnumerable();
        }

        public Employee GetById(int id)
        {
            return _webStoreDb.Employee.FirstOrDefault(e => e.Id == id);
        }

        public bool Delete(int id)
        {
            var emp = _webStoreDb.Employee.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return false;
            _webStoreDb.Employee.Remove(emp);
            return true;
        }

        public void SaveChanges()
        {
            _webStoreDb.SaveChanges();
        }

        public void Edit(int id, Employee emp)
        {
            if (_webStoreDb.Employee.FirstOrDefault(e => e.Id == id) == null)
                return;

            _webStoreDb.Employee.FirstOrDefault(e => e.Id == id).SurName = emp.SurName;
            _webStoreDb.Employee.FirstOrDefault(e => e.Id == id).Age = emp.Age;
            _webStoreDb.Employee.FirstOrDefault(e => e.Id == id).FirstName = emp.FirstName;
            _webStoreDb.Employee.FirstOrDefault(e => e.Id == id).Patronymic = emp.Patronymic;
        }

        public void Add(Employee emp)
        {
            _webStoreDb.Employee.Add(new Employee()
            {
                SurName = emp.SurName,
                Age = emp.Age,
                FirstName = emp.FirstName,
                Patronymic = emp.Patronymic
            });
        }
    }
}

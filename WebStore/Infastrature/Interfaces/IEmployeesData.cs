using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infastrature.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void Add(Employee emp);

        void Edit(int id, Employee emp);

        bool Delete(int id);

        void SaveChanges();

    }
}

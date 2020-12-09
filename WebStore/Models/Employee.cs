using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Employee
    {
        
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Ошибка в имени")]
        [MinLength(3, ErrorMessage = "Ошибка в имени")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string SurName { get; set; }

        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Возраст")]
        public int Age { get; set; }
    }
}

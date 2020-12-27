using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.DomainCore.Entities
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

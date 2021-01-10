using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DomainCore.Entities.Base;
using WebStore.DomainCore.Entities.Identity;

namespace WebStore.DomainCore.Entities.Orders
{
    public class Order : NamedEntity
    {
        [Required]
        public virtual User User { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public bool Completed { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DomainCore.Entities.Base;

namespace WebStore.DomainCore.Entities.Orders
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [Column (TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

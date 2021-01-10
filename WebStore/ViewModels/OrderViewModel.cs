using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string Name { get; set; }

        //    [Column(TypeName = Phon)]
        [Required]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Address { get; set; }
    }
}

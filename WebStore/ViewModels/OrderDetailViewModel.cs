using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class OrderDetailViewModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public CartViewModel CartViewModel { get; set; }
    }
}

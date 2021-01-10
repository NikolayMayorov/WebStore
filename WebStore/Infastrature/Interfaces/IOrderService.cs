using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DomainCore.Entities.Identity;
using WebStore.DomainCore.Entities.Orders;
using WebStore.ViewModels;

namespace WebStore.Infastrature.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrdersByUserName(string name);

        Order GetOrderById(int id);

        Task<Order> CreateOrder(string userName, CartViewModel cartViewModel, OrderViewModel orderViewModel);
    }
}

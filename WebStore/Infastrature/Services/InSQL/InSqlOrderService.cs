using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DAL.Migrations;
using WebStore.DomainCore.Entities.Identity;
using WebStore.DomainCore.Entities.Orders;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infastrature.Services.InSQL
{
    public class InSqlOrderService : IOrderService
    {
        private readonly UserManager<User> _userManager;
        private readonly WebStoreDB _webStoreDb;

        public InSqlOrderService(WebStoreDB webStoreDB, UserManager<User> userManager)
        {
            _webStoreDb = webStoreDB;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetOrdersByUserName(string name)
        {
            return _webStoreDb.Orders.Include(order => order.User).Include(order => order.Items)
                .Where(order => order.User.UserName == name).AsEnumerable();
        }

        public Order GetOrderById(int id)
        {
            return _webStoreDb.Orders.Include(order => order.Items).FirstOrDefault(order => order.Id == id);
        }

        public async Task<Order> CreateOrder(string userName, CartViewModel cartViewModel,
            OrderViewModel orderViewModel)
        {
            var user = await _userManager.FindByNameAsync(userName: userName);

            Order order;

            await using (var transaction = await _webStoreDb.Database.BeginTransactionAsync())
            {
                order = new Order
                {
                    User = user,
                    Address = orderViewModel.Address,
                    Name = orderViewModel.Name,
                    Phone = orderViewModel.Phone,
                    Date = DateTime.Now
                };

                await _webStoreDb.Orders.AddAsync(entity: order);


                foreach (var (productViewModel, quantity) in cartViewModel.Items)
                {
                    var product = await _webStoreDb.Products.FirstOrDefaultAsync(p => p.Id == productViewModel.Id);

                    if (product is null)
                    {
                        return null;
                    }

                    var orderItem = new OrderItem
                    {
                        Order = order,
                        Quantity = quantity,
                        Price = productViewModel.Price,
                        Product = product
                    };
                    await _webStoreDb.OrderItems.AddAsync(orderItem);
                }

            

                await _webStoreDb.SaveChangesAsync();
                await _webStoreDb.Database.CommitTransactionAsync();
            }

            return order;
        }
    }
}
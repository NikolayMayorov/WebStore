using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.DomainCore.Entities;
using WebStore.Expansion;
using WebStore.Infastrature.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infastrature.Services.InCookie
{
    public class InCookieCartService : ICartService
    {
        private readonly string _cartNameCookie;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IProductData _productData;

        public InCookieCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _productData = productData;

            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User.Identity;

            _cartNameCookie = $"CartName[{user?.Name}]";
        }

        private Cart Cart
        {
            get
            {
                var cart = new Cart();
                var cookiesRequest = _httpContextAccessor.HttpContext.Request.Cookies;
                var cookiesResponse = _httpContextAccessor.HttpContext.Response.Cookies;

                if (cookiesRequest[key: _cartNameCookie] is null)
                {
                    cookiesResponse.Append(key: _cartNameCookie, JsonConvert.SerializeObject(value: cart));
                }
                else
                {
                    cookiesResponse.Delete(key: _cartNameCookie);

                    cookiesResponse.Append(key: _cartNameCookie, cookiesRequest[key: _cartNameCookie]);

                    cart = JsonConvert.DeserializeObject<Cart>(cookiesRequest[key: _cartNameCookie]);
                }

                return cart;
            }
            set
            {
                var cart = JsonConvert.SerializeObject(value: value);

                _httpContextAccessor.HttpContext.Response.Cookies.Delete(key: _cartNameCookie);

                _httpContextAccessor.HttpContext.Response.Cookies.Append(key: _cartNameCookie, value: cart);
            }
        }

        public void AddToCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
                cart.Items.Add(new CartItem
                {
                    ProductId = id,
                    Quantity = 1
                });
            else
                item.Quantity++;

            Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
                return;
            if (item.Quantity == 1)
                cart.Items.Remove(item: item);
            else
                item.Quantity--;

            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
        }

        public void RemoveAll()
        {
        }

        public CartViewModel TransformFromCart()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(i => i.ProductId).ToList()
            }).ToList();

            var productsVm = products.Select(p => p.ToView()).ToDictionary(p => p.Id, p => p);



            var cartViewModel = new CartViewModel();
            // cartViewModel.Items = new Dictionary<ProductViewModel, int>();



            cartViewModel.Items = Cart.Items.Where(item => productsVm.ContainsKey(item.ProductId)).ToDictionary(item => productsVm[item.ProductId], c => c.Quantity);
   


            /*
            foreach (var product in products)
            {
                int count;
                if (Cart.Items.FirstOrDefault(i => i.ProductId == product.Id) is null)
                    count = 0;
                else
                    count = Cart.Items.First(i => i.ProductId == product.Id).Quantity;

                cartViewModel.Items.Add(product.ToView(), count);
            }
            */

            return cartViewModel;
        }
    }
}
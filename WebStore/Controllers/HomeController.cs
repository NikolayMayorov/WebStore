using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller //ControllerBase  для веб апи
    {
        
        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

    

        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

       

        public IActionResult Index()
        {
            //var products = _productData.GetProducts();
            //var catalogVM = new CatalogViewModel
            //{
            //    Products = products.Select(p => new ProductViewModel
            //    {
            //        BrandId = p.BrandId,
            //        Order = p.Order,
            //        Name = p.Name,
            //        SectionId = p.SectionId,
            //        Id = p.Id,
            //        ImageUrl = p.ImageUrl,
            //        Price = p.Price
            //    }).OrderBy(_ => _.Order)
            //};

            //catalogVM.Products.OrderBy(p => p.Order);
            //return View(model: catalogVM);
            return View();
        }
   
        public EmptyResult GetEmty()
        {
            return new EmptyResult();
        }

        public ApplicationException GetExc(string id)
        {
            throw new ApplicationException(message: id);
        }
    }
}
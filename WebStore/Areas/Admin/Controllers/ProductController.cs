using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebStore.DomainCore.Entities;
using WebStore.DomainCore.Entities.Identity;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Administrator)]
    public class ProductController : Controller
    {
        private readonly IProductData _productData;

        public ProductController (IProductData productData)
        {
            _productData = productData;
        }


    
        public IActionResult Index()
        {
            return View(_productData.GetProducts());
        }


        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            return RedirectToAction("Index");
        }

     
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                var product = new Product();
                return View(product);
            }
            return View(_productData.GetProductById((int)id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            var product = _productData.GetProductById(model.Id);
            if (product == null)
                return NotFound();
            product.Name = model.Name;
            product.Price = model.Price;
            _productData.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

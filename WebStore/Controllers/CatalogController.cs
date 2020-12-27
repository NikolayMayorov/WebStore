using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.DomainCore.Entities;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult ProductDetails()
        {
   
            return View();
        }

        public IActionResult Shop(int? brandId, int? sectionId)
        {
            var products = _productData.GetProducts(new ProductFilter()
            {
                BrandId = brandId,
                SectionId = sectionId
            });

            var catalogVM = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Select(p => new ProductViewModel()
                {
                    BrandId = p.BrandId,
                    Order = p.Order,
                    Name = p.Name,
                    SectionId = p.SectionId,
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                }).OrderBy( _ => _.Order)
            };
            return View(catalogVM);
        }
    }
}

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

        public IActionResult ProductDetails(int id)
        {
            var product = _productData.GetProductById(id);
            if (product is null)
                return NotFound();

            var productVM = new ProductViewModel()
            {
                Name = product.Name,
                Order = product.Order,
                ImageUrl = product.ImageUrl,
                Id = product.Id,
                BrandId = product.BrandId,
                SectionId = product.SectionId,
                Price = product.Price
                
            };
            return View(productVM);
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

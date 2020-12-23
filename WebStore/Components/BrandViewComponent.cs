using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke()
        {
            return View(GetBrands());
        }

        public IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _productData.GetBrands();

            var brandsVM = new List<BrandViewModel>();

            foreach (var brand in brands)
            {
                brandsVM.Add(new BrandViewModel()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Order = brand.Order
                });
            }

            brandsVM = brandsVM.OrderBy(_ => _.Order).ToList();
            return brandsVM;
        }
    }
}

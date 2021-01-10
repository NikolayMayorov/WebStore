using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DomainCore.Entities;
using WebStore.ViewModels;

namespace WebStore.Expansion
{
    public static class ProductExpansion
    {
        public static ProductViewModel ToView(this Product product)
        {

            return new ProductViewModel()
            {
                Name = product.Name,
                Order = product.Order,
                SectionId = product.SectionId,
                BrandId = product.BrandId,
                Price = product.Price,
                Id = product.Id,
                ImageUrl = product.ImageUrl
            };
        }
    }
}

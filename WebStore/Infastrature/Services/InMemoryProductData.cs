using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.DomainCore.Entities;
using WebStore.Infastrature.Interfaces;

namespace WebStore.Infastrature.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            var result = TestData.Products;

            if (!(filter?.BrandId is null))
            {
                result = result.Where(b => b.BrandId == filter.BrandId);
            }

            if (!(filter?.SectionId is null))
            {
                result = result.Where(b => b.SectionId == filter.SectionId);
            }

            return result;
        }
    }
}

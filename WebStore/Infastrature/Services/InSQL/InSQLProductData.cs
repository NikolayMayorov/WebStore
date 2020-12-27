using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainCore.Entities;
using WebStore.Infastrature.Interfaces;

namespace WebStore.Infastrature.Services.InSQL
{
    public class InSQLProductData : IProductData
    {
        private readonly WebStoreDB _webStoreDb;

        public InSQLProductData(WebStoreDB webStoreDb)
        {
            _webStoreDb = webStoreDb;
        }

        public IEnumerable<Section> GetSections()
        {
            //return _webStoreDb.Sections.Include(section => section.Products).AsEnumerable();
            return _webStoreDb.Sections.AsEnumerable();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _webStoreDb.Brands.Include(section => section.Products).AsEnumerable();
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = _webStoreDb.Products;

            if (filter?.BrandId != null)
            {
                query = query.Where(product => product.BrandId == filter.BrandId);
            }
            if (filter?.SectionId != null)
            {
                query = query.Where(product => product.SectionId == filter.SectionId);
            }

            return query.AsEnumerable();
        }
    }
}

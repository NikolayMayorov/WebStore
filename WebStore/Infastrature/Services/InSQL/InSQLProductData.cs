using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.DomainCore.Entities;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels;

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

            // if (filter?.Ids?.Count > 0)
            if (filter?.Ids != null)
            {
                query = query.Where(product => filter.Ids.Contains(product.Id));
            }

            return query.AsEnumerable();
        }

        public Product GetProductById(int id)
        {
            //  var product = _webStoreDb.Products.FirstOrDefault(p => p.Id == id);
            var product = _webStoreDb.Products.Include(p => p.Brand).Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id);
            return product;
        }

        public void Edit(Product product)
        {
            if (_webStoreDb.Products.FirstOrDefault(p => p.Id == product.Id) is null)
                return;

            var _product = _webStoreDb.Products.First(p => p.Id == product.Id);

            _product.Price = product.Price;
            _product.Name = product.Name;

            _webStoreDb.SaveChanges();
        }

        public void Add(Product product)
        {
        }

        public void Delete(int id)
        {
        }

        public void SaveChanges()
        {
            _webStoreDb.SaveChanges();
        }
    }
}

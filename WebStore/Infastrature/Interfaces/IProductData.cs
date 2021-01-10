using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DomainCore.Entities;

namespace WebStore.Infastrature.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter filter = null);

        Product GetProductById(int id);

        void Edit(Product product);

        void Add(Product product);

        void Delete(int id);

        void SaveChanges();
    }
}

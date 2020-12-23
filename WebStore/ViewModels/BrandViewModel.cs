using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}

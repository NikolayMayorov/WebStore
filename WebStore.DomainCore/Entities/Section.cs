using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.DomainCore.Entities.Base;
using WebStore.DomainCore.Entities.Base.Interfaces;

namespace WebStore.DomainCore.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentSectionId { get; set; }

        [ForeignKey(nameof(ParentSectionId))]
        public virtual Section ParentSection { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

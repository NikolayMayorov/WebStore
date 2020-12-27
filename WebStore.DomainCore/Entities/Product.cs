using System.ComponentModel.DataAnnotations.Schema;
using WebStore.DomainCore.Entities.Base;
using WebStore.DomainCore.Entities.Base.Interfaces;

namespace WebStore.DomainCore.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Desription { get; set; }

    }
}

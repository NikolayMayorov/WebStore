using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.DomainCore.Entities.Base.Interfaces
{
    /// <summary>
    /// Порядковая сущность
    /// </summary>
    public interface IOrderedEntity : IBaseEntity
    {
        /// <summary>
        /// Порядковый номер
        /// </summary>
        int Order { get; set; }
    }
}

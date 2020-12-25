using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.DomainCore.Entities.Base.Interfaces
{
    /// <summary>
    /// Именованная сущность
    /// </summary>
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        string Name { get; set; }
    }
}

using System.Collections.Generic;
using Domain.Common;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interface for entities with Domain Events.
    /// </summary>
    public interface IHasDomainEvent
    {
        /// <summary>
        /// Collection of Entity Domain Events.
        /// </summary>
        public ICollection<DomainEvent> DomainEvents { get; set; }
    }
}
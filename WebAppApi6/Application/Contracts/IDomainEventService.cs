using System.Threading.Tasks;
using Domain.Common;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Interface for service to manage Domain Events.
    /// </summary>
    public interface IDomainEventService
    {
        /// <summary>
        /// Publish Domain Events.
        /// </summary>
        /// <param name="domainEvent">Domain event to publish.</param>
        Task Publish(DomainEvent domainEvent);
    }
}
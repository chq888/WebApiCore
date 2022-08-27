using Domain.Common;
using MediatR;

namespace Application.Common.Models
{
    /// <summary>
    /// Defines notification for domain events.
    /// </summary>
    /// <typeparam name="TDomainEvent">Domain event.</typeparam>
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
    {
        /// <summary>
        /// Domain event.
        /// </summary>
        public TDomainEvent DomainEvent { get; set; }

        /// <summary>
        /// Defines object of <see cref="DomainEventNotification{TDomainEvent}"/> class.
        /// </summary>
        /// <param name="domainEvent">Domain event.</param>
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
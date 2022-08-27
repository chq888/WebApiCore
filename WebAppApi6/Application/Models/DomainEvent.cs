using System;

namespace Domain.Common
{
    /// <summary>
    /// Entity Domain Event.
    /// </summary>
    public abstract class DomainEvent
    {
        /// <summary>
        /// Publication flag.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Event creation date (in UTC format).
        /// </summary>
        public DateTimeOffset DateOccured { get; set; } = DateTime.UtcNow;

        protected DomainEvent()
        {
            DateOccured = DateTime.UtcNow;
        }
    }
}
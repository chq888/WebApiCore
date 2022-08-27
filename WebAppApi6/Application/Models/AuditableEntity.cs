using System;
using Domain.Interfaces;

namespace Domain.Common
{
    /// <summary>
    /// Abstract class for auditable entity.
    /// </summary>
    public abstract class AuditableEntity : ICreatable, IModifiable
    {
        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime? ModifiedAt { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }
    }
}
using System;
namespace Domain.Interfaces
{
    /// <summary>
    /// Interface for entities, that can be created.
    /// </summary>
    public interface ICreatable
    {
        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Creator.
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
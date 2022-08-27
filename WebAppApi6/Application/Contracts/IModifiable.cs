using System;
namespace Domain.Interfaces
{
    /// <summary>
    /// Interface for entities, that can be modified.
    /// </summary>
    public interface IModifiable
    {
        /// <summary>
        /// Last modification date.
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Last modifier.
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
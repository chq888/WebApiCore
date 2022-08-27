using System;
namespace Application.Common.Interfaces
{
    /// <summary>
    /// Interface for service managing Date/Time.
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        /// Current Data and Time.
        /// </summary>
        DateTime Now { get; }
    }
}
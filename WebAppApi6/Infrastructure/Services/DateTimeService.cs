using System;
using Application.Common.Interfaces;

namespace Infrastructure.Services
{
    /// <summary>
    /// Service to get current Date and Time.
    /// </summary>
    public class DateTimeService : IDateTimeService
    {
        /// <inheritdoc/>
        public DateTime Now => DateTime.Now;
    }
}
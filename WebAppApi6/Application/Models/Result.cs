using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Models
{
    /// <summary>
    /// Processing result.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Defines, whether processing succeeded.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Array of processing errors.
        /// </summary>
        public string[] Errors { get; set; }

        /// <summary>
        /// Set success results.
        /// </summary>
        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        /// <summary>
        /// Set failure result.
        /// </summary>
        /// <param name="errors">Processing errors.</param>
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        /// <summary>
        /// Defines object of <see cref="Result"/> class.
        /// </summary>
        /// <param name="succeeded">Is processing succeeded.</param>
        /// <param name="errors">Processing errors.</param>
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }
    }
}
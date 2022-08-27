using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Common
{
    /// <summary>
    /// Base object without ID (https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects)
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Determines whether the specified object is equal to the currect object.
        /// </summary>
        /// <param name="obj">Object to check equality with.</param>
        /// <returns>True if specified object is equal to current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Calculate hash core for the current object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
                return false;
            
            return left?.Equals(right) != false;
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !EqualOperator(left, right);

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
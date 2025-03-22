using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models
{
    public class CorrelatedScopes : IEquatable<CorrelatedScopes>
    {
        /// <summary>
        /// The entity id of the scope item.
        /// </summary>
        public string HarbourEntityId { get; set; }

        /// <summary>
        /// The scope level of the item.
        /// </summary>
        public int ScopeLevelId { get; set; }

        /// <summary>
        /// The CorrelationId.
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(CorrelatedScopes other)
        {
            // Check if right object is null
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            // Check if both objects have same reference
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Do comparison of objects based on values
            return this.HarbourEntityId == other.HarbourEntityId && this.ScopeLevelId == other.ScopeLevelId;
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            // Check if right object is null
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            // Check if both objects have same reference
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Check if both objects are of different types
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            // Do comparison of objects only if both are of same type
            return this.Equals((CorrelatedScopes)obj);
        }
    }
}

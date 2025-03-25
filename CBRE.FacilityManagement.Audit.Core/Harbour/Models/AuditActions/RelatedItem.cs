using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.AuditActions
{
    public class RelatedItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the object id.
        /// </summary>
        public string RelatedObjectId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        public string RelatedObjectType { get; set; } = string.Empty;

        /// <summary>s
        /// Gets or sets the shard.
        /// </summary>
        public override string Shard => $"{this.RelatedObjectType.ToLower()}-{this.RelatedObjectId}";
    }
}

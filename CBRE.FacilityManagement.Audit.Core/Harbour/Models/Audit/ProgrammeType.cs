using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    /// <summary>
    /// The Programme type.
    /// </summary>
    [Collection(nameof(Audit))]
    [Entity(nameof(ProgrammeType))]
    public class ProgrammeType : BaseEntity
    {
        /// <summary>
        /// The container.
        /// </summary>
        public override string Container => nameof(Audit);

        /// <summary>
        /// The container.
        /// </summary>
        public override string Shard => nameof(ProgrammeType);

        /// <summary>
        /// The entity.
        /// </summary>
        public override string Entity => nameof(ProgrammeType);

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the audit type.
        /// </summary>
        public AuditType AuditType { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public List<AuditCategory> Categories { get; set; } = new List<AuditCategory>();
    }
}

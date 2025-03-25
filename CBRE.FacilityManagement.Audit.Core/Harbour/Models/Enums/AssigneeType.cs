using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums
{
    public enum AssigneeType
    {
        /// <summary>
        /// The auditor.
        /// </summary>
        Auditor = 1,
        /// <summary>
        /// The auditee.
        /// </summary>
        Auditee = 2,

        /// <summary>
        /// Action Responsible.
        /// </summary>
        ActionResponsible = 3,

        /// <summary>
        /// Action Verifier.
        /// </summary>
        ActionVerifier = 4
    }
}

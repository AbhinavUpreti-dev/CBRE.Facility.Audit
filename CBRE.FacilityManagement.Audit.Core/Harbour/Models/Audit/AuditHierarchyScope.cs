using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    public class AuditHierarchyScope
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string ClientCountryName { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the ManagingOffice id.
        /// </summary>
        public int ManagingOfficeId { get; set; }

        /// <summary>
        /// Gets or sets the ManagingOffice name.
        /// </summary>
        public string ManagingOfficeName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CBRE.FacilityManagement.Audit.Application.Models.Harbour
{
    public class HierarchyInputModel
    {
        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        [FromQuery(Name = "filter")]
        public List<string> Filters { get; set; }

        [FromQuery(Name = "title")]
        public string AuditTitle { get; set; }

        [FromQuery(Name = "isIFMHub")]
        public bool IsIFMHub { get; set; } = false;

    }
}

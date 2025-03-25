// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scope.cs" company="CBRE">
//   Copyright (c) CBRE. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    /// <summary>
    /// The audit scope.
    /// </summary>
    public class AuditScope : Scope
    {
        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the entity code.
        /// </summary>
        public string EntityCode { get; set; }

        /// <summary>
        /// Gets or sets the SiteAddress.
        /// </summary>
        public string SiteAddress { get; set; }
    }
}
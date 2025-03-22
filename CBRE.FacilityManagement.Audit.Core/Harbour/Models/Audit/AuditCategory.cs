// <copyright file="AuditCategory.cs" company="CBRE">
// Copyright (c) CBRE. All rights reserved.
// </copyright>

using System;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    /// <summary>
    /// The audit category.
    /// </summary>
    public class AuditCategory
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}

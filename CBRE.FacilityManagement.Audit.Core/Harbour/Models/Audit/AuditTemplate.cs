// <copyright file="AuditTemplate.cs" company="CBRE">
// Copyright (c) CBRE. All rights reserved.
// </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The audit template.
    /// </summary>
    public class AuditTemplate
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the selected questions.
        /// </summary>
        public IEnumerable<Selectable<Question>> Questions { get; set; }
    }
}

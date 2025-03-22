// // <copyright file="Assignee.cs" company="CBRE">
// // Copyright (c) CBRE. All rights reserved.
// // </copyright>

using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{

    /// <summary>
    /// The assignee.
    /// </summary>
    public class Assignee
    {
        /// <summary>
        /// Gets or sets the assignee type.
        /// </summary>
        public AssigneeType? Type { get; set; } = AssigneeType.Auditor;

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }
    }
}
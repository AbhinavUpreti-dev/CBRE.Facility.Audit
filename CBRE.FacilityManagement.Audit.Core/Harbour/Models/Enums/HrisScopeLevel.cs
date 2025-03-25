// <copyright file="HrisScope.cs" company="CBRE">
//   Copyright (c) CBRE. All rights reserved.
// </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums
{
    /// <summary>
    /// The hris scope.
    /// </summary>
    public enum HrisScopeLevel
    {
        /// <summary>
        /// The region.
        /// </summary>
        Region = 10,

        /// <summary>
        /// The country.
        /// </summary>
        Country = 11,

        /// <summary>
        /// The business segment.
        /// </summary>
        BusinessSegment = 12,

        /// <summary>
        /// The line of business.
        /// </summary>
        SubBusinessSegment = 13,

        /// <summary>
        /// The business division.
        /// </summary>
        BusinessDivision = 14,

        /// <summary>
        /// The sub division.
        /// </summary>
        SubDivision = 19,

        /// <summary>
        /// The client.
        /// </summary>
        Client = 15,

        /// <summary>
        /// The business unit.
        /// </summary>
        ManagingOffice = 16,

        /// <summary>
        /// The business specific.
        /// </summary>
        Location = 17,

        /// <summary>
        /// The site.
        /// </summary>
        ClientMO = 18
    }
}
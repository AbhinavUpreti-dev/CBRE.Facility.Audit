// <copyright file="Scope.cs" company="CBRE">
// Copyright (c) CBRE. All rights reserved.
// </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums
{
    /// <summary>
    /// The scope.
    /// </summary>
    public enum ErpScopeLevel
    {
        /// <summary>
        /// The line of business.
        /// </summary>
        LineOfBusiness = 1,

        /// <summary>
        /// The business segment.
        /// </summary>
        BusinessSegment = 2,

        /// <summary>
        /// The business division.
        /// </summary>
        BusinessDivision = 3,

        /// <summary>
        /// The business unit.
        /// </summary>
        BusinessUnit = 4,

        /// <summary>
        /// The client.
        /// </summary>
        Client = 5,

        /// <summary>
        /// The contract.
        /// </summary>
        Contract = 6,

        /// <summary>
        /// The site.
        /// </summary>
        Site = 7,

        /// <summary>
        /// The region.
        /// </summary>
        Region = 8,

        /// <summary>
        /// The country.
        /// </summary>
        Country = 9
    }
}
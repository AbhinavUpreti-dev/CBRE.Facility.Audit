// <copyright file="FindingType.cs" company="CBRE">
// Copyright (c) CBRE. All rights reserved.
// </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    /// <summary>
    /// The finding type.
    /// </summary>
    [Collection(nameof(Audit))]
    [Entity(nameof(FindingType))]
    public class FindingType : BaseEntity
    {
        /// <summary>
        /// The container.
        /// </summary>
        public override string Container => nameof(Audit);

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}

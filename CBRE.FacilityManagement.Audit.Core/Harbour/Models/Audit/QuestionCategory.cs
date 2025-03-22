// <copyright file="QuestionCategory.cs" company="CBRE">
// Copyright (c) CBRE. All rights reserved.
// </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    /// <summary>
    /// The question category.
    /// </summary>
    [Collection(nameof(Audit))]
    [Entity(nameof(QuestionCategory))]
    public class QuestionCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }

}

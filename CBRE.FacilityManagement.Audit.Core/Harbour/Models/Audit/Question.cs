// <copyright file="Question.cs" company="CBRE">
// Copyright (c) CBRE. All rights reserved.
// </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The question.
    /// </summary>
    [Collection("Audit")]
    [Entity("Questions")]
    public class Question : BaseEntity
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the supporting text.
        /// </summary>
        public string SupportingText { get; set; }

        /// <summary>
        /// Gets or sets the desired answer.
        /// </summary>
        public DesiredAnswer DesiredAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include not applicable option.
        /// </summary>
        public bool IncludeNotApplicableOption { get; set; }

        /// <summary>
        /// Gets or sets the supporting url.
        /// </summary>
        public string SupportingUrl { get; set; }

        /// <summary>
        /// Gets or sets the question category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the finding Type.
        /// </summary>
        public FindingType FindingType { get; set; }

        /// <summary>
        /// Gets or sets the audit type.
        /// </summary>
        public AuditType? AuditType { get; set; }

        /// <summary>
        /// Gets or sets the Programme type.
        /// </summary>
        public ProgrammeType ProgrammeType { get; set; }

        /// <summary>
        /// Gets or sets the Shard for Questions.
        /// </summary>
        public override string Shard { get; set; } = "Questions";

        /// <summary>
        /// Gets or sets the created userEmail.
        /// </summary>
        public string CreatedByUserEmail { get; set; }

        /// <summary>
        /// Gets or sets the created date time with DateTime type.
        /// </summary>
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the original Question Id if current entity is a copied version.
        /// </summary>
        public string RelatedQuestionId { get; set; }

        /// <summary>
        /// Gets or sets the associated question sets.
        /// </summary>
        public List<string> AssociatedQuestionSets { get; set; } = new List<string>();
    }
}

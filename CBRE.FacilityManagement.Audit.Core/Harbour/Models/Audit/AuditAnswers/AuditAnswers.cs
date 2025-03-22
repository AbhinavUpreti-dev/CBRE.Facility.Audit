using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit.AuditAnswers
{
    /// <summary>
    /// The site audit.
    /// </summary>
    [Collection("Audit")]
    [Entity("SiteAudit")]
    public class AuditAnswers : BaseEntity
    {
        /// <summary>
        /// The shard.
        /// </summary>
        public override string Shard => this.AuditId;

        /// <summary>
        /// Gets or sets the audit id.
        /// </summary>
        public string AuditId { get; set; }

        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public ZonedDateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        public List<AuditAnswer> Answers { get; set; } = new List<AuditAnswer>();

        /// <summary>
        /// Gets or sets a value indicating whether is custom template.
        /// </summary>
        public bool? IsCustomTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Site Audit Entity.
        /// </summary>
        public new string Entity = "SiteAudit";

        /// <summary>
        /// Gets or sets the root cause.
        /// </summary>
        public string RootCause { get; set; }

        /// <summary>
        /// Gets or sets the user comment.
        /// </summary>
        public string UserComment { get; set; }

        /// <summary>
        /// Gets or sets the Next Question Index while conducting the audit.
        /// </summary>
        public int NextAuditQuestionIndex { get; set; }

        /// <summary>
        /// Gets or sets the completion date when audit is completed.
        /// </summary>
        public ZonedDateTime? CompletionDate { get; set; }
    }
}

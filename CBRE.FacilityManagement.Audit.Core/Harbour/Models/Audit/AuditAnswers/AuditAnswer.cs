using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit.AuditAnswers
{
    /// <summary>
    /// The audit answer.
    /// </summary>
    public class AuditAnswer
    {
        /// <summary>
        /// Gets or sets the question id.
        /// </summary>
        public string QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the question text.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        public AuditAnswerResponse? Response { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is desired answer.
        /// </summary>
        public bool? IsDesiredAnswer { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the finding details.
        /// </summary>
        public string FindingDetails { get; set; }

        /// <summary>
        /// Gets or sets the answered by user id.
        /// </summary>
        public string AnsweredByUserId { get; set; }

        /// <summary>
        /// Gets or sets the root cause.
        /// </summary>
        public string RootCause { get; set; }

        /// <summary>
        /// Gets or sets the answered by name.
        /// </summary>
        public string AnsweredByName { get; set; }

        /// <summary>
        /// Gets or sets the answered date.
        /// </summary>
        public virtual string AnsweredDate { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets whether the Question is flagged or not.
        /// </summary>
        public bool IsFlagged { get; set; }

    }
}

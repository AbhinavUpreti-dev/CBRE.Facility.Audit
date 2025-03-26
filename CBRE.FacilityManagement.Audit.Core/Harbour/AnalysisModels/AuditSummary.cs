using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.AnalysisModels
{
    public class AuditSummary
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the audit type.
        /// </summary>
        public string AuditType { get; set; }

        /// <summary>
        /// Gets or sets the Programme type.
        /// </summary>
        public string ProgrammeType { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the audit questions.
        /// </summary>
        public IEnumerable<string> AuditQuestions { get; set; }

        /// <summary>
        /// Gets or sets the audit templates.
        /// </summary>
        public IEnumerable<string> AuditTemplates { get; set; }

        /// <summary>
        /// Gets or sets StartDate
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets EndDate
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Gets or sets the Assigned by user id.
        /// </summary>
        public string AssignedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the assignees.
        /// </summary>
        public List<Assignee> Assignees { get; set; } = new List<Assignee>();

        // <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the site code.
        /// </summary>
        public string SiteCode { get; set; }

        /// <summary>
        /// Gets or sets the site name.
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the site address.
        /// </summary>
        public string SiteAddress { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public string Coordinates { get; set; }

        public string Status { get; set; }

        public List<QuestionAnswers> QuestionAnswers { get; set; } = new List<QuestionAnswers>();

        // public List<ActionSummary> Actions { get; set; } = new List<ActionSummary>();

    }
}

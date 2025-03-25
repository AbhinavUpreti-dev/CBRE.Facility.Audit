using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
using NodaTime;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    /// <summary>
    /// The audit.
    /// </summary>
    [Collection("Audit")]
    [Entity("Audit")]
    public class Audit : BaseEntity, IHasStates
    {
        /// <summary>
        /// Gets or sets the protocol type.
        /// </summary>
        public AuditProtocolType ProtocolType { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the audit type.
        /// </summary>
        public AuditType AuditType { get; set; }

        /// <summary>
        /// Gets or sets the Programme type.
        /// </summary>
        public ProgrammeType ProgrammeType { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the audit questions.
        /// </summary>
        public IEnumerable<Question> AuditQuestions { get; set; }

        /// <summary>
        /// Gets or sets the audit templates.
        /// </summary>
        public IEnumerable<AuditTemplate> AuditTemplates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is draft.
        /// </summary>
        public bool IsDraft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is recurring.
        /// </summary>
        public bool IsRecurring { get; set; }

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

        /// <summary>
        /// Gets or sets the scopes.
        /// </summary>
        public IEnumerable<AuditScope> Scopes { get; set; } = new List<AuditScope>();

        /// <summary>
        /// Gets or Sets whether template or not.
        /// </summary>
        public bool IsTemplate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether audit is project related or not.
        /// </summary>
        /// <value>
        /// Is project related.
        /// </value>
        public string IsProjectRelated { get; set; }

        /// <summary>
        /// Gets or sets the ProjectContractNumber.
        /// </summary>
        public string ProjectContractNumber { get; set; }

        /// <summary>
        /// Gets or sets the Observation Scope.
        /// </summary>
        public AuditHierarchyScope AuditHierarchyScope { get; set; }

        /// <summary>
        /// get set IsPendingReview.
        /// </summary>
        public bool IsPendingReview { get; set; }

        /// <summary>
        /// Gets or sets flow scope value indicating whether to show client&location or only location.
        /// </summary>
        /// </value>
        public int? FlowScope { get; set; }

        /// <summary>
        /// Gets or sets the deleted occurences for a Scheduled Audit
        /// </summary>
        public List<string> DeletedOccurrences { get; set; } = new List<string>();

        // <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is custom template.
        /// </summary>
        public bool? IsCustomTemplate { get; set; }

        /// <summary>
        /// Gets or sets Parent Custom Form TemplateIds.
        /// </summary>
        public List<string> ParentCustomFormTemplateIds { get; set; } = new List<string>();

        /// <summary>
        /// The correlated scopes for accessibility of record.
        /// </summary>
        public List<CorrelatedScopes> CorrelatedScopes { get; set; } = new List<CorrelatedScopes>();

        /// <summary>
        /// Gets or sets the IsCollaborative.
        /// </summary>
        public bool? IsCollaborative { get; set; }

        /// <summary>
        /// Gets or sets the site entity id.
        /// </summary>
        public int? SiteEntityId { get; set; }

        /// <summary>
        /// Gets or sets the site code.
        /// </summary>
        public string SiteId { get; set; }

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

        /// <summary>
        /// Gets or sets the AssuranceQuestionsTemplateType.
        /// </summary>
        public string AssuranceQuestionsTemplateType { get; set; }

        /// <summary>
        /// Gets or sets the IsPartialComplete.
        /// </summary>
        public bool? IsPartialComplete { get; set; }

        /// <summary>
        /// These are the states of the Audit.
        /// Will populate this list on Audit Creation and manage them based on the Audit Progress.
        /// </summary>
        public List<EntityState> States { get; set; } = new List<EntityState>();

        /// <summary>
        /// Determines whether this instance is in open status.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is in open status; otherwise, <c>false</c>.
        /// </returns>
        public bool IsOpenState()
        {
            var currentStatus = States != null && States.Count > 0 ? States.Find(x => x.IsActive)?.Name : string.Empty;
            return currentStatus == AuditStatus.Open.ToString() || currentStatus == AuditStatus.Started.ToString() || currentStatus == AuditStatus.InProgress.ToString() || currentStatus == AuditStatus.OnHold.ToString();
        }
    }
}

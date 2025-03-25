using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    public class ActionSummary
    {
        public string QuestionTitle { get; set; } = string.Empty;

        public string ActionDescription { get; set; }

        public string RootCause { get; set; } = string.Empty;

        public DateTime ActionDueDate { get; set; }

        public string ActionStatus { get; set; } = string.Empty;

        public bool IsVerificationRequired { get; set; }
    }
}

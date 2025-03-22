using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    public class QuestionAnswers
    {
        public string Question { get; set; } = string.Empty;

        public bool IsDesiredAnswer { get; set; }

        public string Reference { get; set; }

        public string FindingDetails { get; set;; }
    }
}

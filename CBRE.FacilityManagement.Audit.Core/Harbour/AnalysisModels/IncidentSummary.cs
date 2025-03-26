using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.AnalysisModels
{
    public class IncidentSummary
    {
        public string NameOfInjured { get; set; }

        public string DateOfIncident { get; set; }

        public string TreatmentProvided { get; set; }
        public string IncidentDescription { get; set; }

        public bool ClaimRaised { get; set; }

        public string IncidentClassification { get; set; }

        public string Status { get; set; }

        public bool IsEndOfLifeEvent { get; set; }

        public string HowInjuryHappened { get; set; }

        public string IncidentType { get; set; }

        public string NonInjuryType { get; set; }

        public string Title { get; set; }

        public string Reason { get; set; }

        public List<ActionSummary> IncidentActions = new List<ActionSummary>();

    }
}

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

        public string ClientName { get; set; }

        public string SiteName { get; set; }

        public string SiteAddress { get; set; }

        public string TreatmentProvided { get; set; }
        public string IncidentDescription { get; set; }
        public string bodyPartsInjured { get; set; }

        public bool ClaimRaised { get; set; }

        public string IncidentClassification { get; set; }

        public string Status { get; set; }

        public bool IsEndOfLifeEvent { get; set; }

        public string HowInjuryHappened { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Models.Documents
{
    public class DocumentRequest
    {
        public string? CustomerName { get; set; }
        public string? ContractName { get; set; }
        public string? BuildingName { get; set; }
        public string? DocumentGroup { get; set; } = "C - Electrical Safety Logbook";
        public string? DocumentSubGroup { get; set; } = "010 Electrical Installation Certificates (including Minor Works)";
        public string? UserId { get; set; } = "aupreti";
        public bool? showFullSummary { get; set; } = false;
    }
}

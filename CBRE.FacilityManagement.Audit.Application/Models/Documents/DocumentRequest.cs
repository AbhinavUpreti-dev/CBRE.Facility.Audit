using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Models.Documents
{
    public class DocumentRequest
    {
        public Guid? EntityId { get; set; }
        public int? DocumentGroupId { get; set; }
        public string? Name { get; set; }
        public string? MimeType { get; set; }
        public string? Extension { get; set; }
        public bool? IsActive { get; set; }
    }
}

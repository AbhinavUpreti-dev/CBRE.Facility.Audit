using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.DTO.Elogbook
{
    public class DocumentDTO
    {
        public string Recommendations { get; set; }
        public List<ActionDTO> Actions { get; set; }
    }
    public class ActionDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? RequiredDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; }
    }
}

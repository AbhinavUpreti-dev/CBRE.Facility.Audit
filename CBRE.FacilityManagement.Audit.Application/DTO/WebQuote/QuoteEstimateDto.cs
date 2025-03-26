using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.DTO.WebQuote
{
    public class QuoteEstimateDto
    {
        public string Category{ get; set; }
        public string GroupDescription { get; set; }
        public string SubGroupDescription { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }

}

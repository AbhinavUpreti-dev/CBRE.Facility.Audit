using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.DTO.Harbour
{
    public class HierarchyInputFilter
    {
        public int BusinessSegmentId { get; set; }

        public int SubBusinessSegmentId { get; set; }

        public int RegionId { get; set; }

        public int CountryId { get; set; }

        public int DivisionId { get; set; }

        public int ManagingOfficeId { get; set; }

        public int ClientId { get; set; }

        public int LocationId { get; set; }
    }
}

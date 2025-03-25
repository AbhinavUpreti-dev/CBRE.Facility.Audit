using CBRE.FacilityManagement.Audit.Application.Models.Harbour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Features.Harbour.Interfaces
{
    public interface IAuditAppService
    {
        Task<SummaryModel> GetHierarchySummary(HierarchyInputModel inputModel);
    }
}

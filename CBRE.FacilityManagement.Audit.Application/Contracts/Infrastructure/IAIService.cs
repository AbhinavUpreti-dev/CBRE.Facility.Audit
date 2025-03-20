using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Contracts.Infrastructure
{
   public interface IAIService
    {
        Task<string> GenerateSummaryAsync(List<string> documents);
    }
}

using CBRE.FacilityManagement.Audit.Core.WebQuote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces
{
    public interface IWebQuoteRepository
    {
        Task<List<QuoteEstimate>> GetQuoteEstimatesAsync(string clientName, string locationDescription, string contractReference);
    }
}

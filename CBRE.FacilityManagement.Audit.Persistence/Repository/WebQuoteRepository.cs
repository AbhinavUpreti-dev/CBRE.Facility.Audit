using CBRE.FacilityManagement.Audit.Core.WebQuote;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository
{
    public class WebQuoteRepository : IWebQuoteRepository
    {
        private readonly WebQuoteDatabaseContext _context;

        public WebQuoteRepository(WebQuoteDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<QuoteEstimate>> GetQuoteEstimatesAsync(string clientName, string locationDescription, string contractReference)
        {
            var query = _context.QuoteEstimate
                .Include(qe => qe.QuoteType).DefaultIfEmpty()
                .Include(qe => qe.Group).DefaultIfEmpty()
                .Include(qe => qe.SubGroup).DefaultIfEmpty()
                .Include(qe => qe.Status).DefaultIfEmpty()
                .Include(qe => qe.Client).DefaultIfEmpty()
                .Include(qe => qe.Contract).DefaultIfEmpty()
                .Include(qe => qe.ContractLocation)
                    .ThenInclude(cl => cl.Location).DefaultIfEmpty()
                .AsQueryable();

            if (!string.IsNullOrEmpty(clientName))
            {
                query = query.Where(qe => qe.Client.Name.Contains(clientName));
            }

            if (!string.IsNullOrEmpty(locationDescription))
            {
                query = query.Where(qe => qe.ContractLocation.Location.Description == locationDescription);
            }

            if (!string.IsNullOrEmpty(contractReference))
            {
                query = query.Where(qe => qe.Contract.Reference == contractReference);
            }

            return await query.ToListAsync();
        }
    }
}

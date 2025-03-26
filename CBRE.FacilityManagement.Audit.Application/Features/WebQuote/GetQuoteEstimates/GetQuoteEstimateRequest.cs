using CBRE.FacilityManagement.Audit.Application.DTO.WebQuote;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Features.WebQuote.GetQuoteEstimates
{

    public class GetQuoteEstimatesQuery : IRequest<List<QuoteEstimateDto>>
    {
        public string ClientName { get; set; }
        public string LocationDescription { get; set; }
        public string ContractReference { get; set; }
    }

}

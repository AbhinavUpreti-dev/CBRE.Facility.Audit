using CBRE.FacilityManagement.Audit.Application.DTO.WebQuote;
using CBRE.FacilityManagement.Audit.Persistence.Repository;
using CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Features.WebQuote.GetQuoteEstimates
{
    public class GetQuoteEstimatesHandler : IRequestHandler<GetQuoteEstimatesQuery, List<QuoteEstimateDto>>
    {
        private readonly IWebQuoteRepository _repository;

        public GetQuoteEstimatesHandler(IWebQuoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<QuoteEstimateDto>> Handle(GetQuoteEstimatesQuery request, CancellationToken cancellationToken)
        {
            var quoteEstimates = await _repository.GetQuoteEstimatesAsync(request.ClientName, request.LocationDescription, request.ContractReference);

            return quoteEstimates.Take(10).Select(qe => new QuoteEstimateDto
            {
                Category = qe.QuoteType?.Name,
                GroupDescription = qe.Group?.GroupDescription,
                SubGroupDescription = qe.SubGroup?.SubGroupDescription,
                Status = qe.Closed?"InActive":"Active",
                Description = qe.Description               
            }).ToList();
        }
    }
}

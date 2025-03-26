using CBRE.FacilityManagement.Audit.Application.DTO.WebQuote;
using CBRE.FacilityManagement.Audit.Application.Features.WebQuote.GetQuoteEstimates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBRE.FacilityManagement.Audit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebQuoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WebQuoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetQuoteEstimates")]
        public async Task<ActionResult<List<QuoteEstimateDto>>> GetQuoteEstimates([FromQuery] GetQuoteEstimatesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

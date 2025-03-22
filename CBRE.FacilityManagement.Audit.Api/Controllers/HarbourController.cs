using Microsoft.AspNetCore.Mvc;
using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers;
using MediatR;

namespace CBRE.FacilityManagement.Audit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarbourController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HarbourController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAuditSummary")]
        public async Task<ActionResult> GetCustomers()
        {
            var query = new GetCustomerQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using MediatR;
using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBRE.FacilityManagement.Audit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElogBookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ElogBookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("customers")]
        public async Task<ActionResult<List<CustomerDTO>>> GetCustomers()
        {
            var query = new GetCustomerQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }
    }
}

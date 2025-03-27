using Microsoft.AspNetCore.Mvc;
using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using MediatR;
using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers;
using CBRE.FacilityManagement.Audit.Application.Models.Documents;
using CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetDocumentSummary;
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

        [HttpPost("recommendations")]
        public async Task<ActionResult<List<DocumentDTO>>> GetDocuments([FromBody] DocumentRequest request)
        {
            var query = new GetDocumentSummary(request);
            var documents = await _mediator.Send(query);
            return Ok(documents);
        }

        //[HttpPost("recommendations/{optionalValue?}")]
        //public async Task<ActionResult<List<DocumentDTO>>> GetDocuments([FromBody] DocumentRequest request, string optionalValue = null)
        //{
        //    if (string.IsNullOrEmpty(optionalValue))
        //    {
        //        // Logic when optionalValue is not provided
        //        var query = new GetDocumentSummary(request);
        //        var documents = await _mediator.Send(query);
        //        return Ok(documents);
        //    }
        //    else
        //    {
        //        // Logic when optionalValue is provided
        //        // Modify the request or query based on optionalValue
        //        var modifiedRequest = ModifyRequestBasedOnOptionalValue(request, optionalValue);
        //        var query = new GetDocumentSummary(modifiedRequest);
        //        var documents = await _mediator.Send(query);
        //        return Ok(documents);
        //    }
        //}
    }
}

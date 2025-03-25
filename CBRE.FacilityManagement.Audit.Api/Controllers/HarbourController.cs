using Microsoft.AspNetCore.Mvc;
using CBRE.FacilityManagement.Audit.Application.Features.Harbour.Interfaces;
using CBRE.FacilityManagement.Audit.Application.Models.Harbour;

namespace CBRE.FacilityManagement.Audit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarbourController : ControllerBase
    {
        private readonly IAuditAppService auditAppService;

        public HarbourController(IAuditAppService auditAppService)
        {
            this.auditAppService = auditAppService;
        }
        [HttpGet("GetAuditSummary/{ct?}")]
        public async Task<ActionResult> GetAuditSummary(HierarchyInputModel inputModel)
        {
            var result = await auditAppService.GetHierarchySummary(inputModel);
            return Ok(result);
        }
    }
}

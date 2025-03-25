using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using CBRE.FacilityManagement.Audit.Application.Models.Documents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetDocumentSummary
{
    public record GetDocumentSummary : IRequest<List<DocumentDTO>>
    {
        public DocumentRequest Request { get; set; }

        public GetDocumentSummary(DocumentRequest request)
        {
            Request = request;
        }
    }
}

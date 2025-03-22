using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetDocumentSummary
{
    public class GetDocumentSummaryHandler : IRequestHandler<GetDocumentSummary, List<DocumentDTO>>
    {
        private readonly IELogBookRepository _repository;
        private readonly IDocumentSummarizerAIService _summarizerAIService;

        public GetDocumentSummaryHandler(IELogBookRepository repository, IDocumentSummarizerAIService summarizerAIService)
        {
            _repository = repository;
            _summarizerAIService = summarizerAIService;
        }

        public async Task<List<DocumentDTO>> Handle(GetDocumentSummary request, CancellationToken cancellationToken)
        {
            var documents = await _repository.GetDocumentsAsync(
                request.Request.EntityId,
                request.Request.DocumentGroupId,
                request.Request.Name,
                request.Request.MimeType,
                request.Request.Extension,
                request.Request.IsActive);

            var document = documents.FirstOrDefault();
            if (document == null)
            {
                return new List<DocumentDTO>();
            }

            var fileData = document.FileData;
            var fileExtension = document.Extension;
            var filePath = $"input{fileExtension}";

            // Save the byte array as a file
            await File.WriteAllBytesAsync(filePath, fileData, cancellationToken);

            try
            {
                var aiSummary = _summarizerAIService.GenerateSummaryAsync(new List<string> { filePath }, fileExtension);

                return new List<DocumentDTO> { new DocumentDTO { DocumentSummary = aiSummary } };
            }
            finally
            {
                // Ensure the file is deleted after processing
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}

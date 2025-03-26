using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
            var (docId, docSubGroupId) =  ValidateRequest(request);
            var documents = await _repository.GetDocumentsAsync(
                request.Request.CustomerName,
                request.Request.ContractName,
                request.Request.BuildingName,
               docId,
               docSubGroupId,request.Request.UserId);

            if (documents == null)
            {
                // Handle the case where no document is found
                throw new Exception("No document found for the given criteria.");
            }

            List<string> filePaths = new List<string>();
            List<DocumentDTO> documentDTOs = new List<DocumentDTO>();
            string aiSummary = "";
            try
            {
                foreach (var document in documents)
                {
                    var fileData = document.FileData;
                    var fileExtension = document.Extension;
                    var path = $"input{fileExtension}";
                    filePaths.Add(path);

                    // Save the byte array as a file
                    File.WriteAllBytes(path, fileData);
                    var summary =  _summarizerAIService.GenerateSummaryAsync(new List<string> { path }, fileExtension);
                  //  aiSummary += $"AI Generated Recommendation {document.Name}:\n{summary}\n\n";
                   
                    var actions = await _repository.GetActionsByDocumentGroupIdAsync(document.DocumentGroupId.Value);

                    var actionDTOs = actions.Select(a => new ActionDTO
                    {
                        Id = a.Id,
                        Description = a.Description,
                        Status = a.Status,
                        RequiredDate = a.RequiredDate,
                        ClosedDate = a.ClosedDate
                    }).ToList();

                    documentDTOs.Add(new DocumentDTO
                    {
                        Recommendations = summary,
                        Actions = actionDTOs
                    });
                }
                return documentDTOs;
            }
            finally
            {
                foreach (var filePath in filePaths)
                {
                    // Ensure the file is deleted after processing
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
        }

        private (int docId, int docSubGroupId) ValidateRequest(GetDocumentSummary request)
        {
            if (string.IsNullOrEmpty(request.Request.DocumentGroup))
            {
                throw new ArgumentException("DocumentGroup is required.");
            }
            var docId = 0;
            var docSubId = 0;
            if (!string.IsNullOrEmpty(request.Request.DocumentSubGroup))
            {
                var documentGroup = _repository.GetDocumentGroupByName(request.Request.DocumentGroup);
                if (documentGroup == null || documentGroup.ParentId != null)
                {
                    docId = documentGroup.Id;
                    throw new ArgumentException("Invalid DocumentGroup specified.");
                }

                var documentSubGroup = _repository.GetDocumentGroupByName(request.Request.DocumentSubGroup);
               if(documentSubGroup is not null)
                {
                    docSubId = documentSubGroup.Id;
                }
            }
            return (docId,docSubId);
        }
    }
}

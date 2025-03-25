using CBRE.FacilityManagement.Audit.Application.Features.Harbour.Interfaces;
using CBRE.FacilityManagement.Audit.Persistence.CosmosDbRespository;
using CBRE.FacilityManagement.Audit.Core.Harbour;
using CBRE.FacilityManagement.Audit.Core.Harbour.AnalysisModels;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Incident;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit;
using Newtonsoft.Json;
using CBRE.FacilityManagement.Audit.Application.Models.Harbour;
using CBRE.FacilityManagement.Audit.Core.Harbour.Helper;

namespace CBRE.FacilityManagement.Audit.Application.Features.Harbour.Services
{
    public class AuditAppService : IAuditAppService
    {
        private readonly IRepository _dbRepository;
        private readonly IDocumentSummarizerAIService documentSummarizerAIService;

        public AuditAppService(IRepository _dbRepository, IDocumentSummarizerAIService documentSummarizerAIService)
        {
            this._dbRepository = _dbRepository;
            this.documentSummarizerAIService = documentSummarizerAIService;
        }

        public async Task<SummaryModel> GetHierarchySummary(HierarchyInputModel inputModel)
        {
            SummaryModel response = new SummaryModel();
            Dictionary<int, int> filters;
            try
            {
                filters = InputModelHelper.GetDecodedFilters(inputModel.Filters);
            }
            catch (ArgumentException e)
            {
                throw;
            }
            response.AuditSummary = await GetAuditSummary(filters);
            response.IncidentSummary = await GetIncidentSummary(filters);
            return response;
        }

        public async Task<string> GetAuditSummary(Dictionary<int, int> filters)
        {
            
            List<AuditSummary> audits = new List<AuditSummary>();
            string auditSummaryJson = string.Empty;
            string auditFinalSummary = string.Empty;

            
            string auditQuery = "SELECT * FROM c " +
                " where c.Container = 'Audit'" +
                " AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 17,\r\n    \"HarbourEntityId\": '628665'\r\n}, true)" +
                " AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 10,\r\n    \"HarbourEntityId\": '3'\r\n}, true)" +
                " order by c._ts desc";

            string sqlQuery = $@"
                                SELECT * FROM c 
                                WHERE c.Container = 'Audit'
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 17,
                                    ""HarbourEntityId"": '{filters[17]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 10,
                                    ""HarbourEntityId"": '{filters[10]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 11,
                                    ""HarbourEntityId"": '{filters[11]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 12,
                                    ""HarbourEntityId"": '{filters[12]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 13,
                                    ""HarbourEntityId"": '{filters[13]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 14,
                                    ""HarbourEntityId"": '{filters[14]}'
                                }}, true)   
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 15,
                                    ""HarbourEntityId"": '{filters[15]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 16,
                                    ""HarbourEntityId"": '{filters[16]}'
                                }}, true)
                                ORDER BY c._ts DESC
                                ";


            CustomQueryResponse<Core.Harbour.Models.Audit.Audit> result = await this._dbRepository.GetTokenizedAsync<Core.Harbour.Models.Audit.Audit, Core.Harbour.Models.Audit.Audit>(sqlQuery, true, 100, null, null);

            foreach (var audit in result.Data)
            {

                var auditAssignees = audit.Assignees.Select(x => new Assignee()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    AuditAssigneeType = Enum.GetName(typeof(AssigneeType), x.Type),
                }).ToList();

                var auditSummary = new AuditSummary()
                {
                    Title = audit.Title,
                    AuditType = Enum.GetName(typeof(AuditType), audit.AuditType),
                    ProgrammeType = audit.ProgrammeType.Name,
                    Category = audit.Category.Name,
                    AuditTemplates = audit.AuditTemplates.SelectMany(a => a.Questions.Where(x => x.IsSelected)).Select(x => x.Instance).Select(x => x.Content).ToList(),
                    StartDate = audit.StartDate,
                    EndDate = audit.EndDate,
                    AssignedByUserId = audit.AssignedByUserId,
                    Assignees = auditAssignees,
                    ClientName = audit.ClientName ?? audit.AuditHierarchyScope?.ClientCountryName,
                    SiteName = audit.SiteName,
                    SiteAddress = audit.SiteAddress,
                    Status = audit.States?.FirstOrDefault(x => x.IsActive)?.Name,
                };

                string auditAnswersQuery = $"SELECT * FROM c " +
                " where c.Container = 'AuditAnswers'" +
                $" and c.Shard = '{audit.Id}'" +
                " order by c._ts desc";

                CustomQueryResponse<Core.Harbour.Models.Audit.AuditAnswers.AuditAnswers> auditAnswersData = await this._dbRepository.GetTokenizedAsync<Core.Harbour.Models.Audit.AuditAnswers.AuditAnswers, Core.Harbour.Models.Audit.AuditAnswers.AuditAnswers>(auditAnswersQuery, false);

                foreach (var auditAnswer in auditAnswersData.Data)
                {
                    foreach (var answer in auditAnswer.Answers)
                    {
                        auditSummary.QuestionAnswers.Add(new QuestionAnswers()
                        {
                            Question = answer.QuestionText,
                            IsDesiredAnswer = answer.IsDesiredAnswer ?? false,
                            FindingDetails = answer.FindingDetails,
                            Reference = answer.Reference,
                            QuestionResponse = Enum.GetName(typeof(AuditAnswerResponse), answer.Response),
                        });
                    }
                }

                string auditActionQuery = $"SELECT * FROM c " +
                " where c.RelatedObjectType = 'answeraudit' and c.Entity = 'Action'" +
                $" and SUBSTRING(c.RelatedObjectId, 0, 36) = '{audit.Id}'" +
                " order by c._ts desc";

                CustomQueryResponse<Core.Harbour.Models.AuditActions.Action> auditActionData = await this._dbRepository.GetTokenizedAsync<Core.Harbour.Models.AuditActions.Action, Core.Harbour.Models.AuditActions.Action>(auditActionQuery, false);

                foreach (var action in auditActionData.Data)
                {
                    var actionFormData = action.CreateStronglyTypedFormData();
                    auditSummary.Actions.Add(new ActionSummary()
                    {
                        QuestionTitle = actionFormData.QuestionTitle,
                        ActionStatus = action.States?.FirstOrDefault(x => x.IsActive)?.Name,
                        ActionDueDate = actionFormData.DueDate,
                        ActionDescription = actionFormData.Description,
                        RootCause = actionFormData.RootCause,
                        IsVerificationRequired = actionFormData.VerificationRequired,
                    });
                }

                audits.Add(auditSummary);
            }

            if (audits.Count > 0)
            {
                auditSummaryJson = JsonConvert.SerializeObject(audits);
                return await this.documentSummarizerAIService.GenerateAuditSummaryAsync(auditSummaryJson);
            }
            return "";
        }

        public async Task<string> GetIncidentSummary(Dictionary<int, int> filters)
        {
            List<IncidentSummary> incidents = new List<IncidentSummary>();
            string incidentSummaryJson = string.Empty;
            string incidentFinalSummary = string.Empty;
            string incidentQuery = $@"SELECT * FROM c 
                where c.Entity = 'InjuryIllnessIncident'
                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 17,
                                    ""HarbourEntityId"": '{filters[17]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 10,
                                    ""HarbourEntityId"": '{filters[10]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 11,
                                    ""HarbourEntityId"": '{filters[11]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 12,
                                    ""HarbourEntityId"": '{filters[12]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 13,
                                    ""HarbourEntityId"": '{filters[13]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 14,
                                    ""HarbourEntityId"": '{filters[14]}'
                                }}, true)   
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 15,
                                    ""HarbourEntityId"": '{filters[15]}'
                                }}, true)
                                AND ARRAY_CONTAINS(c.CorrelatedScopes, {{
                                    ""ScopeLevelId"": 16,
                                    ""HarbourEntityId"": '{filters[16]}'
                                }}, true)
                order by c._ts desc";
            CustomQueryResponse<Incident> result = await this._dbRepository.GetTokenizedAsync<Incident, Incident>(incidentQuery, false);

            foreach (var incident in result.Data)
            {
                var formData = incident.CreateStronglyTypedFormData();
                incidents.Add(
                    new IncidentSummary()
                    {
                        NameOfInjured = formData.NameOfInjured?.ToString() ?? formData.NameOfInjuredText,
                        DateOfIncident = formData.IncidentDate?.ToLongDateString(),
                        ClientName = formData?.LocationDetails?.ClientName,
                        SiteName = formData?.LocationDetails?.SiteName,
                        SiteAddress = formData?.LocationDetails?.SiteAddress,
                        IncidentClassification = formData?.Classification,
                        IncidentDescription = formData.InjuryDescription,
                        ClaimRaised = formData.ClaimRaised,
                        TreatmentProvided = formData.TreatmentType,
                        HowInjuryHappened = formData.HowInjuryHappened,
                        IsEndOfLifeEvent = formData.IsEndOfLifeEvent,
                        Status = incident.States?.FirstOrDefault(x => x.IsActive)?.Name,
                    });
            }
            if (incidents.Count > 0)
            {
                incidentSummaryJson = JsonConvert.SerializeObject(incidents);
                return await this.documentSummarizerAIService.GenerateAuditSummaryAsync(incidentSummaryJson);
            }
            return "";
        }
        
    }
}

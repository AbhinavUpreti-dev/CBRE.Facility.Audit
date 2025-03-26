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
using MediatR;
using CBRE.FacilityManagement.Audit.Core.IFMHub;

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
            if (!inputModel.IsIFMHub)
            {
                response.AuditSummary = await GetAuditSummary(filters);
                response.IncidentSummary = await GetIncidentSummary(filters);
                return response;
            }
            else
            {
                response.AuditSummary = "";
                response.IncidentSummary = await GetIncidentSummary(filters, true);
                return response;
            }

        }

        public async Task<string> GetAuditActionSummary(HierarchyInputModel inputModel)
        {
            Dictionary<int, int> filters;
            List<ActionSummary> actions = new List<ActionSummary>();
            try
            {
                filters = InputModelHelper.GetDecodedFilters(inputModel.Filters);
            }
            catch (ArgumentException e)
            {
                throw;
            }
            string sqlQuery = $@"
                                SELECT * FROM c 
                                WHERE c.Container = 'Audit'
                                AND c.Title = '{inputModel.AuditTitle}'
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
                string auditActionQuery = $"SELECT * FROM c " +
                " where c.RelatedObjectType = 'answeraudit' and c.Entity = 'Action'" +
                $" and SUBSTRING(c.RelatedObjectId, 0, 36) = '{audit.Id}'" +
                " order by c._ts desc";

                CustomQueryResponse<Core.Harbour.Models.AuditActions.Action> auditActionData = await this._dbRepository.GetTokenizedAsync<Core.Harbour.Models.AuditActions.Action, Core.Harbour.Models.AuditActions.Action>(auditActionQuery, false);

                foreach (var action in auditActionData.Data)
                {
                    var actionFormData = action.CreateStronglyTypedFormData();
                    actions.Add(new ActionSummary()
                    {
                        QuestionTitle = actionFormData.QuestionTitle,
                        ActionStatus = action.States?.FirstOrDefault(x => x.IsActive)?.Name,
                        ActionDueDate = actionFormData.DueDate,
                        ActionDescription = actionFormData.Description,
                        RootCause = actionFormData.RootCause,
                        IsVerificationRequired = actionFormData.VerificationRequired,
                    });
                }
            }
            if (actions.Count > 0)
            {
                string auditActionPrompt = "Summarize the following JSON data: {0}.Please provide a detailed summary including the key points of the json without including any words such as json or datasets in response.";
                string auditActionSummaryJson = JsonConvert.SerializeObject(actions);
                return await this.documentSummarizerAIService.GenerateAuditSummaryAsync(auditActionSummaryJson, auditActionPrompt, "");
            }
            return "";
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

                audits.Add(auditSummary);
            }

            if (audits.Count > 0)
            {
                string auditSummaryPrompt = "Summarize the key findings from this data {0}. Identify common issues across these audit reports without using the words such as json in the response.";
                auditSummaryJson = JsonConvert.SerializeObject(audits);
                return await this.documentSummarizerAIService.GenerateAuditSummaryAsync(auditSummaryJson, auditSummaryPrompt, "");
            }
            return "";
        }

        public async Task<string> GetIncidentSummary(Dictionary<int, int> filters, bool isIFMHub = false)
        {
            List<IncidentSummary> incidents = new List<IncidentSummary>();
            string incidentSummaryJson = string.Empty;
            string incidentFinalSummary = string.Empty;
            string assetJson = "";
            List<AssetModel> assets = new List<AssetModel>();
            string incidentQuery = $@"SELECT * FROM c 
                where ARRAY_CONTAINS(c.CorrelatedScopes, {{
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

                var incidentData = new IncidentSummary()
                {
                    NameOfInjured = formData.NameOfInjured?.ToString() ?? formData.NameOfInjuredText,
                    DateOfIncident = formData.IncidentDate?.ToLongDateString(),
                    IncidentClassification = formData?.Classification,
                    IncidentDescription = formData.InjuryDescription,
                    ClaimRaised = formData.ClaimRaised,
                    TreatmentProvided = formData.TreatmentType,
                    HowInjuryHappened = formData.HowInjuryHappened,
                    IsEndOfLifeEvent = formData.IsEndOfLifeEvent,
                    Status = incident.States?.FirstOrDefault(x => x.IsActive)?.Name,
                    IncidentType = incident.Entity,
                    NonInjuryType = formData.NonInjuryType,
                    Title = formData.Title,
                    Reason = formData.Reason,
                };

                string incidentActionQuery = $"SELECT * FROM c " +
               " where c.RelatedObjectType = 'incident' and c.Entity = 'Action'" +
               $" and c.RelatedObjectId = '{incident.Id}'" +
               " order by c._ts desc";

                CustomQueryResponse<Core.Harbour.Models.AuditActions.Action> incidentActionData = await this._dbRepository.GetTokenizedAsync<Core.Harbour.Models.AuditActions.Action, Core.Harbour.Models.AuditActions.Action>(incidentActionQuery, false);

                foreach (var action in incidentActionData.Data)
                {
                    var actionFormData = action.CreateStronglyTypedFormData();
                    incidentData.IncidentActions.Add(new ActionSummary()
                    {
                        QuestionTitle = actionFormData.QuestionTitle,
                        ActionStatus = action.States?.FirstOrDefault(x => x.IsActive)?.Name,
                        ActionDueDate = actionFormData.DueDate,
                        ActionDescription = actionFormData.Description,
                        RootCause = actionFormData.RootCause,
                        IsVerificationRequired = actionFormData.VerificationRequired,
                    });
                }

                incidents.Add(incidentData);

            }

            if (isIFMHub)
            {
                assets.AddRange(new List<AssetModel>() { new AssetModel()
                {
                    AssetDescription = "Air conditioner(Air Conditioner ACU489 | SHANG | Shanghai 536 Rong Le Road | Adminstration Office | 2nd Floor - Common Area)",
                    AssetManufacturer = "UNKNOWN",
                    DateInstalled = "2021-01-01",
                    AssetModelType = "UNKNOWN"
                }, new AssetModel()
                {
                    AssetDescription = "Packaged Terminal Air Conditioner(RTU-04)",
                    AssetManufacturer = "Trane",
                    DateInstalled = "2022-01-01",
                    AssetModelType = "UNKNOWN"
                }, new AssetModel()
                {
                    AssetDescription = "Motor Control Center, <600V(Variable Frequency Drive -  Exhaust Fan AHU North Roof 2B)",
                    AssetManufacturer = "FRANKLIN CONTROLS",
                    DateInstalled = "2021-01-01",
                    AssetModelType = "UNKNOWN"
                }, new AssetModel()
                {
                    AssetDescription = "080-FPU - PUMP - FIRE(B18359-080-FPU-001)",
                    AssetManufacturer = "UNKNOWN",
                    DateInstalled = "2024-01-01",
                    AssetModelType = "UNKNOWN"
                }, new AssetModel()
                {
                    AssetDescription = "Ice Machine(ICEM-0691ICY-01)",
                    AssetManufacturer = "Hoshizaki",
                    DateInstalled = "2024-01-01",
                    AssetModelType = "UNKNOWN"
                }
                });

                assetJson = JsonConvert.SerializeObject(assets);
            }
            if (incidents.Count > 0)
            {
                string prompt = !isIFMHub ? "Based on these incident reports and actions taken provided in the json: {0}, what type of audit should be conducted at this site ? Identify the key areas that need auditing for this site without using the words such as json in the response.":
                     "Based on these incident reports present in the json: {0}, predict if any assets present in the assets json : {1} might be damaged.Determine if an audit is necessary for these assets based on past incidents without using the words such as json in the response.";

                // string incidentSummaryPrompt = "Summarize the following JSON data: {0}.Please provide a detailed summary including the key points of the json without including any words such as json or datasets in response.";
                incidentSummaryJson = JsonConvert.SerializeObject(incidents);
                return await this.documentSummarizerAIService.GenerateAuditSummaryAsync(incidentSummaryJson, prompt, assetJson);
            }
            return "";
        }

    }
}

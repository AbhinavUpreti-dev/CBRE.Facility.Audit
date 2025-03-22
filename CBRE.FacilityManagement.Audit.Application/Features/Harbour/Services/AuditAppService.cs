using CBRE.FacilityManagement.Audit.Application.Features.Harbour.Interfaces;
using CBRE.FacilityManagement.Audit.Persistence.CosmosDbRespository;
using CBRE.FacilityManagement.Audit.Core.Harbour;
using CBRE.FacilityManagement.Audit.Core.Harbour.AnalysisModels;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Incident;
using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit;
using Newtonsoft.Json;

namespace CBRE.FacilityManagement.Audit.Application.Features.Harbour.Services
{
    public class AuditAppService : IAuditAppService
    {
        private readonly IRepository _dbRepository;

        public AuditAppService(IRepository _dbRepository)
        {
            this._dbRepository = _dbRepository;
        }

        public async Task GetAuditSummary()
        {
            List<AuditSummary> audits = new List<AuditSummary>();
            string auditSummaryJson = string.Empty;
            string auditQuery = "SELECT * FROM c " +
                "where c.Container = 'Audit'" +
                "AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 17,\r\n    \"HarbourEntityId\": '444520'\r\n}, true)" +
                "AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 10,\r\n    \"HarbourEntityId\": '3'\r\n}, true)" +
                "order by c._ts desc";

            
            CustomQueryResponse<Core.Harbour.Models.Audit.Audit> result = await this._dbRepository.GetTokenizedAsync<Core.Harbour.Models.Audit.Audit, Core.Harbour.Models.Audit.Audit>(auditQuery, false);

            foreach (var audit in result.Data)
            {
                
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
                    Assignees = audit.Assignees,
                    ClientName = audit.ClientName ?? audit.AuditHierarchyScope?.ClientCountryName,
                    SiteName = audit.SiteName,
                    SiteAddress = audit.SiteAddress,
                    Status = audit.States?.FirstOrDefault(x => x.IsActive)?.Name,
                };

                string auditAnswersQuery = $"SELECT * FROM c " +
                "where c.Container = 'AuditAnswers'" +
                $"and c.Shard = '{audit.Id}'" +
                "order by c._ts desc";

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
                        });
                    }
                }

                string auditActionQuery = $"SELECT * FROM c " +
                "where c.RelatedObjectType = 'answeraudit' and c.Entity = 'Action'" +
                $"and SUBSTRING(c.RelatedObjectId, 0, 36) = '{audit.Id}'" +
                "AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 17,\r\n    \"HarbourEntityId\": '444520'\r\n}, true)" +
                "AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 10,\r\n    \"HarbourEntityId\": '3'\r\n}, true)" +
                "order by c._ts desc";

                CustomQueryResponse<Core.Harbour.Models.AuditActions.Action> auditActionData = await this._dbRepository.GetTokenizedAsync<Core.Harbour.Models.AuditActions.Action, Core.Harbour.Models.AuditActions.Action>(auditActionQuery, false);

                foreach (var action in auditActionData.Data)
                {
                    var actionFormData = action.CreateStronglyTypedFormData();
                    auditSummary.Actions.Add(new ActionSummary()
                    {
                        QuestionTitle = actionFormData.QuestionTitle,
                        ActionStatus = actionFormData.ActionCurrentStatus,
                        ActionDueDate = actionFormData.DueDate,
                        ActionDescription = actionFormData.Description,
                        RootCause = actionFormData.RootCause,
                    });
                }

                audits.Add(auditSummary);
            }

            if(audits.Count > 0)
            {
                auditSummaryJson = JsonConvert.SerializeObject(audits);
            }
        }

        public async Task GetIncidentSummary()
        {
            List<IncidentSummary> incidents = new List<IncidentSummary>();
            string incidentQuery = "SELECT * FROM c " +
                "where c.Entity = 'InjuryIllnessIncident'" +
                "AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 17,\r\n    \"HarbourEntityId\": '444520'\r\n}, true)" +
                "AND ARRAY_CONTAINS(c.CorrelatedScopes, {\r\n    \"ScopeLevelId\": 10,\r\n    \"HarbourEntityId\": '3'\r\n}, true)" +
                "order by c._ts desc";
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
        }
    }
}

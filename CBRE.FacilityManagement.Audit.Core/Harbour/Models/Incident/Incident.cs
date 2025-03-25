// // <copyright file="Incident.cs" company="CBRE">
// // Copyright (c) CBRE. All rights reserved.
// // </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Incident
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CBRE.FacilityManagement.Audit.Core.Harbour.Common;
    using Newtonsoft.Json;

    /// <summary>
    /// The Incident.
    /// </summary>
    [Collection("Incidents")]
    [Entity("Incident")]
    public class Incident : BaseEntity, IHasStates
    {
        /// <summary>
        /// The incident form data.
        /// </summary>
        private IncidentFormData incidentFormData;

        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets form data
        /// </summary>
        public dynamic FormData { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string IncidentTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is draft.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is draft; otherwise, <c>false</c>.
        /// </value>
        public bool IsDraft { get; set; }

        /// <summary>
        /// Gets or sets form data
        /// </summary>
        public dynamic MetaData { get; set; }

        /// <summary>
        /// Gets or sets Observation Id
        /// </summary>
        public string ObservationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is checked in.
        /// </summary>
        public bool IsCheckedIn { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        /// <value>
        /// The creator.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the shard.
        /// </summary>
        public override string Shard => this.Id;

        /// <summary>
        /// The correlated scopes for accessibility of record.
        /// </summary>
        public List<CorrelatedScopes> CorrelatedScopes { get; set; } = new List<CorrelatedScopes>();

        public IncidentFormData CreateStronglyTypedFormData()
        {
            if (this.incidentFormData == null)
            {
                this.incidentFormData = IncidentFormData.From(this.FormData);
            }

            return this.incidentFormData;
        }



        /// <summary>
        /// The incident form data.
        /// </summary>
        public class IncidentFormData
        {
            /// <summary>
            /// The form data
            /// </summary>
            protected readonly Dictionary<string, object> formData;

            /// <summary>
            /// Initializes a new instance of the <see cref="IncidentFormData"/> class.
            /// </summary>
            /// <param name="formData">The form data.</param>
            public IncidentFormData(List<KeyValuePair<string, object>> formData)
            {
                this.formData = new Dictionary<string, object>(formData, StringComparer.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// Gets or sets the record identifier.
            /// </summary>
            /// <value>
            /// The record identifier.
            /// </value>
            public string RecordId
            {
                get => this.formData.GetValueAsString("recordId");
                set => this.formData.UpsertValue("recordId", value);
            }

            /// <summary>
            /// Gets or sets the created by.
            /// </summary>
            /// <value>
            /// The created by.
            /// </value>
            public string CreatedBy
            {
                get => this.formData.GetValueAsString("createdBy");
                set => this.formData.UpsertValue("createdBy", value);
            }

            /// <summary>
            /// Gets or sets the company name.
            /// </summary>
            public string CompanyName
            {
                get => this.formData.GetValueAsString("companyName");
                set => this.formData.UpsertValue("companyName", value);
            }

            /// <summary>
            /// Gets or sets the name of injured.
            /// </summary>
            public NameOfInjuredWrapper NameOfInjured
            {
                get => this.formData.GetValue<NameOfInjuredWrapper>("nameOfinjured");
                set => this.formData.UpsertValue("nameOfinjured", value);
            }

            /// <summary>
            /// Gets or sets the name of injured (when person type is Tenant/MOP/Contractor).
            /// </summary>
            public string NameOfInjuredText
            {
                get => this.formData.GetValueAsString("nameOfInjuredText");
                set => this.formData.UpsertValue("nameOfInjuredText", value);
            }

            /// <summary>
            /// Gets or sets the person type.
            /// </summary>
            public string PersonType
            {
                get => this.formData.GetValueAsString("personType");
                set => this.formData.UpsertValue("personType", value);
            }

            /// <summary>
            /// Gets or sets the email address.
            /// </summary>
            public string EmailAddress
            {
                get => this.formData.GetValueAsString("emailAddress");
                set => this.formData.UpsertValue("emailAddress", value);
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is open.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is open; otherwise, <c>false</c>.
            /// </value>
            public bool IsOpen
            {
                get => this.formData.GetValueAsBooleanWithDefault("supervisor.detail.incidentStatusIsOpen", true);
                set => this.formData.UpsertValue("supervisor.detail.incidentStatusIsOpen", value);
            }

            public string isProjectRelated
            {
                get => this.formData.GetValueAsString("incident.projectRelated");
                set => this.formData.UpsertValue("incident.projectRelated", value);
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance can close.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance can close; otherwise, <c>false</c>.
            /// </value>
            public bool ClaimRaised
            {
                get => this.formData.GetValueAsBoolean("furtherDetails.raiseClaim");
                set => this.formData.UpsertValue("furtherDetails.raiseClaim", value);
            }

            /// <summary>
            /// Gets or sets a value indicating whether proceeded with litigation.
            /// </summary>
            public bool ProceededWithLitigation
            {
                get => this.formData.GetValueAsBoolean("litigation.proceedToLitigation");
                set => this.formData.UpsertValue("litigation.proceedToLitigation", value);
            }

            /// <summary>
            /// Gets or sets the return to work comments.
            /// </summary>
            public string ReturnToWorkComments
            {
                get => this.formData.GetValueAsString("returnToWork.comments");
                set => this.formData.UpsertValue("returnToWork.comments", value);
            }

            /// <summary>
            /// Gets or sets a value indicating whether work place incident.
            /// </summary>
            public bool WorkPlaceIncident
            {
                get => this.formData.GetValueAsBoolean("injuryIllnessCoverType");
                set => this.formData.UpsertValue("injuryIllnessCoverType", value);
            }

            /// <summary>
            /// Gets or sets a value TreatmentTypeOtherDescription.
            /// </summary>
            public string TreatmentTypeOtherDescription
            {
                get => this.formData.GetValueAsString("injury.treatmentType.liquid.dropDown.label.other.description");
                set => this.formData.UpsertValue("injury.treatmentType.liquid.dropDown.label.other.description", value);
            }

            /// <summary>
            /// Gets a value indicating whether a claim was raised.
            /// </summary>
            /// <value>
            ///   <c>true</c> if a claim was raised; otherwise, <c>false</c>.
            /// </value>
            public bool? CanClose
            {
                get => this.formData.GetValueAsBoolean("incidentCanClose");
                set => this.formData.UpsertValue("incidentCanClose", value);
            }

            /// <summary>
            /// Gets or sets the classification.
            /// </summary>
            public string Classification
            {
                get => this.formData.GetValueAsString("Classification"); 
                set => this.formData.UpsertValue("Classification", value);
            }

            /// <summary>
            /// Gets or sets the  manual classification selected by user
            /// </summary>
            public string ManualClassification
            {
                get => this.formData.GetValueAsString("manualClassification");
                set => this.formData.UpsertValue("manualClassification", value);
            }

            /// <summary>
            /// Gets or sets is vaccinated.
            /// </summary>
            [Obsolete("This property is obsolete. Use IsCovid19Vaccinated property", false)]
            public string IsVaccinated
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.isVaccinated");
                set => this.formData.UpsertValue("covidSelfAssessment.isVaccinated", value);
            }

            /// <summary>
            /// Gets or sets is GWSVaccinated.
            /// </summary>
            [Obsolete("This property is obsolete. Use IsCovid19Vaccinated property", false)]
            public string IsGWSVaccinated
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.isGWSVaccinated");
                set => this.formData.UpsertValue("covidSelfAssessment.isGWSVaccinated", value);
            }

            /// <summary>
            /// Gets or sets is Covid19Vaccinated.
            /// </summary>
            public string IsCovid19Vaccinated
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.isCovid19Vaccinated");
                set => this.formData.UpsertValue("covidSelfAssessment.isCovid19Vaccinated", value);
            }

            /// <summary>
            /// Gets or sets is Covid19Tested.
            /// </summary>
            public string IsCovid19Tested
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.isCovid19Tested");
                set => this.formData.UpsertValue("covidSelfAssessment.isCovid19Tested", value);
            }

            /// <summary>
            /// Gets or sets covid19TestDate.
            /// </summary>
            public DateTime Covid19TestDate
            {
                get => this.formData.GetValueAsDateTime("covidSelfAssessment.covid19TestDate");
                set => this.formData.UpsertValue("covidSelfAssessment.covid19TestDate", value);
            }

            /// <summary>
            /// Gets or sets Covid19VaccinationDate.
            /// </summary>
            public DateTime Covid19VaccinationDate
            {
                get => this.formData.GetValueAsDateTime("covidSelfAssessment.vaccinationBoosterDate");
                set => this.formData.UpsertValue("covidSelfAssessment.vaccinationBoosterDate", value);
            }

            /// <summary>
            /// Gets or sets is gwsResidentOf.
            /// </summary>
            public string GwsResidentOf
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.gwsResidentOf");
                set => this.formData.UpsertValue("covidSelfAssessment.gwsResidentOf", value);
            }

            /// <summary>
            /// Gets or sets is GWSInformed.
            /// </summary>
            public string IsGWSInformed
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.isGWSInformed");
                set => this.formData.UpsertValue("covidSelfAssessment.isGWSInformed", value);
            }

            /// <summary>
            /// Gets or sets is PM Location.
            /// </summary>
            public bool IsPMLocation
            {
                get => this.formData.GetValueAsBoolean("covidSelfAssessment.isPMLocation");
                set => this.formData.UpsertValue("covidSelfAssessment.isPMLocation", value);
            }

            /// <summary>
            /// Gets or sets is Latam Location.
            /// </summary>
            public string CovidSelfAssessmentRegion
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.region");
                set => this.formData.UpsertValue("covidSelfAssessment.region", value);
            }

            /// <summary>
            /// Gets or sets is GWS Location.
            /// </summary>
            public bool IsGWSLocation
            {
                get => this.formData.GetValueAsBoolean("covidSelfAssessment.isGWSLocation");
                set => this.formData.UpsertValue("covidSelfAssessment.isGWSLocation", value);
            }

            /// <summary>
            /// Gets or sets CountryCode.
            /// </summary>
            public string CountryCode
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.country");
                set => this.formData.UpsertValue("covidSelfAssessment.country", value);
            }

            /// <summary>
            /// Gets or sets Location Code.
            /// </summary>
            public string LocationCode
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.locationCode");
                set => this.formData.UpsertValue("covidSelfAssessment.locationCode", value);
            }

            /// <summary>
            /// Gets or sets Incident Status.
            /// </summary>
            public string Status
            {
                get => this.formData.GetValueAsString("incident.status");
                set => this.formData.UpsertValue("incident.status", value);
            }

            /// <summary>
            /// Gets or sets whether Incident is editable or not.
            /// </summary>
            public bool? IsEditable
            {
                get => this.formData.GetValueAsNullableBoolean("incident.isEditable");
                set => this.formData.UpsertValue("incident.isEditable", value);
            }

            /// <summary>
            /// Gets or sets the return to work.
            /// </summary>
            /// <value>
            /// The return to work.
            /// </value>
            public ReturnToWorkWrapper ReturnToWork => new ReturnToWorkWrapper(this.formData);

            /// <summary>
            /// The from.
            /// </summary>
            /// <param name="formData">
            /// The form data.
            /// </param>
            /// <returns>
            /// The <see cref="IncidentFormData"/>.
            /// </returns>
            public static IncidentFormData From(dynamic formData)
            {
                var kvps =
                    JsonConvert.DeserializeObject<List<KeyValuePair<string, object>>>(formData.ToString());
                return new IncidentFormData(kvps);
            }

            /// <summary>
            /// The is contractor.
            /// </summary>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public bool IsContractor()
            {
                return this.PersonType != null && this.PersonType.Contains(
                           "Contractor",
                           StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// Gets or sets the date claim submitted.
            /// </summary>
            public DateTime DateClaimSubmitted
            {
                get => this.formData.GetValueAsDateTime("DateClaimSubmitted");
                set => this.formData.UpsertValue("DateClaimSubmitted", value);
            }

            /// <summary>
            /// Gets or sets the observation occured date.
            /// </summary>
            public DateTime ObservationOccuredDate
            {
                get => this.formData.GetValueAsDateTime("observationOccuredDate");
                set => this.formData.UpsertValue("observationOccuredDate", value);
            }


            /// <summary>
            /// Gets or sets APACNoVacNoTest.
            /// </summary>
            public string Covid19AttestationOptions
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.covid19AttestationOptions");
                set => this.formData.UpsertValue("covidSelfAssessment.covid19AttestationOptions", value);
            }

            /// <summary>
            /// Gets or sets Covid19TestingOption.
            /// </summary>
            public string Covid19TestingOption
            {
                get => this.formData.GetValueAsString("covidSelfAssessment.testedOrDiagnosed");
                set => this.formData.UpsertValue("covidSelfAssessment.testedOrDiagnosed", value);
            }

            /// <summary>
            /// The is myself.
            /// </summary>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public bool IsMyself()
            {
                return this.PersonType != null && this.PersonType.Contains(
                           "Myself",
                           StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// The is CBRE Employee.
            /// </summary>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public bool IsCBREEmployee()
            {
                return this.PersonType != null && this.PersonType.Contains(
                           "CBRE Employee",
                           StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// The to dynamic.
            /// </summary>
            /// <returns>
            /// The <see cref="dynamic"/>.
            /// </returns>
            public dynamic ToDynamic()
            {
                return JsonConvert.DeserializeObject<dynamic>(
                    JsonConvert.SerializeObject(this.formData.ToList(), FormDataSerializer.JsonSerializerSettings));
            }

            /// <summary>
            /// Add key value to dynamic form data.
            /// </summary>
            /// <param name="pair"></param>
            public void AddKeyValuePair(KeyValuePair<string, object> keyValuePair)
            {
                if (this.formData.ContainsKey(keyValuePair.Key))
                {
                    this.formData.UpsertValue(keyValuePair.Key, keyValuePair.Value);
                }
                else
                {
                    this.formData.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            #region Incident Bifurcation

            /// <summary>
            /// Gets or sets IncidentTitle.
            /// </summary>
            public string Title
            {
                get => this.formData.GetValueAsString("incident.incidentDetails.title");
                set => this.formData.UpsertValue("incident.incidentDetails.title", value);
            }

            /// <summary>
            /// Gets or sets IncidentReason.
            /// </summary>
            public string Reason
            {
                get => this.formData.GetValueAsString("incident.incidentDetails.reason");
                set => this.formData.UpsertValue("incident.incidentDetails.reason", value);
            }

            /// <summary>
            /// Gets or sets IncidentFlow Scope.
            /// </summary>
            public int? FlowScope
            {
                get => this.formData.GetValueAsInt("incident.incidentDetails.flowScope");
                set => this.formData.UpsertValue("incident.incidentDetails.flowScope", value);
            }

            /// <summary>
            /// Gets or sets the RecordableDate
            /// </summary>
            public DateTime? RecordableDate
            {
                get => this.formData.GetValueAsDateTime("incident.classification.recordableDate");
                set => this.formData.UpsertValue("incident.classification.recordableDate", value);
            }

            /// <summary>
            /// Gets or sets the location details.
            /// </summary>
            public LocationDetailsContent LocationDetails
            {
                get => this.formData.GetValue<LocationDetailsContent>("incident.locationDetails");
                set => this.formData.UpsertValue("incident.locationDetails", value);
            }

            public DateTime? IncidentDate
            {
                get => this.formData.GetValueAsDateTime("incident.incidentDetails.whenDate");
                set => this.formData.UpsertValue("incident.incidentDetails.whenDate", value);
            }

            public bool ExternallyTreated
            {
                get => this.formData.GetValueAsBooleanWithDefault("externalMedicalCoverType", false);
                set => this.formData.UpsertValue("externalMedicalCoverType", value);
            }

            public bool InjuryIllnessCoverType
            {
                get => this.formData.GetValueAsBooleanWithDefault("injuryIllnessCoverType", false);
                set => this.formData.UpsertValue("injuryIllnessCoverType", value);
            }

            public string TreatmentType
            {
                get => this.formData.GetValueAsString("personalInjury.typeOfTreatment");
                set => this.formData.UpsertValue("personalInjury.typeOfTreatment", value);
            }

            public bool? HospitalAdmitted
            {
                get => this.formData.GetValueAsNullableBoolean("personalInjury.wasHospitalAdmitted");
                set => this.formData.UpsertValue("personalInjury.wasHospitalAdmitted", value);
            }

            public string HospitalizationType
            {
                get => this.formData.GetValueAsString("personalInjury.hospitalizationType");
                set => this.formData.UpsertValue("personalInjury.hospitalizationType", value);
            }

            public bool FirstAddTreatmentGiven
            {
                get => this.formData.GetValueAsBooleanWithDefault("firstAidTreatmentGiven", false);
                set => this.formData.UpsertValue("firstAidTreatmentGiven", value);
            }

            /// <summary>
            /// Gets or sets the SevereInjury.
            /// </summary>
            public bool SevereInjury
            {
                get => this.formData.GetValueAsBooleanWithDefault("raiseClaim.severeInjury", false);
                set => this.formData.UpsertValue("raiseClaim.severeInjury", value);
            }

            /// <summary>
            /// Gets or sets the initialTreatment.
            /// </summary>
            public string InitialTreatment
            {
                get => this.formData.GetValueAsString("raiseClaim.initialTreatment");
                set => this.formData.UpsertValue("raiseClaim.initialTreatment", value);
            }

            /// <summary>
            /// Gets or sets the HowInjuryHappened.
            /// </summary>
            public string HowInjuryHappened
            {
                get => this.formData.GetValueAsString("injuryHappenValueAndStatement");
                set => this.formData.UpsertValue("injuryHappenValueAndStatement", value);
            }

            /// <summary>
            /// Gets or sets the IsEndOfLifeEvent.
            /// </summary>
            public bool IsEndOfLifeEvent
            {
                get => this.formData.GetValueAsBooleanWithDefault("workerCare.endOfLifeEvent", false);
                set => this.formData.UpsertValue("workerCare.endOfLifeEvent", value);
            }

            /// <summary>
            /// Gets or sets the InjuryDescription.
            /// </summary>
            public string InjuryDescription
            {
                get => this.formData.GetValueAsString("raiseClaim.injuryDescription");
                set => this.formData.UpsertValue("raiseClaim.injuryDescription", value);
            }
            #endregion
        }

        /// <summary>
        /// The list of witnesses.
        /// </summary>
        public class ListOfWitnesses : List<Incident.WitnessWrapper>
        {
            /// <summary>
            /// Pseudonymises the specified count.
            /// </summary>
            /// <param name="count">The count.</param>
            public ListOfWitnesses Pseudonymise(ref int count)
            {
                foreach (var witness in this)
                {
                    if (!witness.IsCbreWitness())
                    {
                        witness.Pseudonymise(++count);
                    }
                }

                return this;
            }
        }

        /// <summary>
        /// The witness wrapper.
        /// </summary>
        public class WitnessWrapper
        {
            /// <summary>
            /// Gets or sets the witness.
            /// </summary>
            /// <value>
            /// The witness.
            /// </value>
            public Witness Witness { get; set; }

            /// <summary>
            /// Pseudonymises the specified count.
            /// </summary>
            /// <param name="count">The count.</param>
            public void Pseudonymise(int count)
            {
                this.Witness.Pseudonymise(count);
            }

            /// <summary>
            /// Determines whether [is cbre witness].
            /// </summary>
            /// <returns>
            ///   <c>true</c> if [is cbre witness]; otherwise, <c>false</c>.
            /// </returns>
            public bool IsCbreWitness()
            {
                return this.Witness.IsCbreWitness();
            }
        }

        public class Witness
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the display name.
            /// </summary>
            /// <value>
            /// The display name.
            /// </value>
            public string DisplayName { get; set; }

            /// <summary>
            /// Gets or sets the name of the company.
            /// </summary>
            /// <value>
            /// The name of the company.
            /// </value>
            public string CompanyName { get; set; }

            /// <summary>
            /// Gets or sets the email address.
            /// </summary>
            /// <value>
            /// The email address.
            /// </value>
            public string EmailAddress { get; set; }

            /// <summary>
            /// Gets or sets the first name.
            /// </summary>
            /// <value>
            /// The first name.
            /// </value>
            public string FirstName { get; set; }

            /// <summary>
            /// Gets or sets the last name.
            /// </summary>
            /// <value>
            /// The last name.
            /// </value>
            public string LastName { get; set; }

            /// <summary>
            /// Gets or sets the contact number.
            /// </summary>
            /// <value>
            /// The contact number.
            /// </value>
            public string ContactNumber { get; set; }

            /// <summary>
            /// Gets or sets the witness statement.
            /// </summary>
            /// <value>
            /// The witness statement.
            /// </value>
            public string WitnessStatement { get; set; }            

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="Witness"/> is pseudonymised.
            /// </summary>
            /// <value>
            ///   <c>true</c> if pseudonymised; otherwise, <c>false</c>.
            /// </value>
            public bool? Pseudonymised { get; set; }

            /// <summary>
            /// Pseudonymises the specified count.
            /// </summary>
            /// <param name="count">The count.</param>
            public void Pseudonymise(int count)
            {
                this.Pseudonymised = true;
                this.DisplayName = $"ThirdParty ({count}) witness statement";
                this.CompanyName = "xxx";
                this.EmailAddress = "xxx";
                this.ContactNumber = "xxx";
            }

            /// <summary>
            /// Determines whether [is cbre witness].
            /// </summary>
            /// <returns>
            ///   <c>true</c> if [is cbre witness]; otherwise, <c>false</c>.
            /// </returns>
            public bool IsCbreWitness()
            {
                return !string.IsNullOrEmpty(this.FirstName) || !string.IsNullOrEmpty(this.LastName);
            }
        }
        
        public class ReturnToWorkWrapper
        {
            /// <summary>
            /// The form data
            /// </summary>
            private readonly Dictionary<string, object> formData;

            /// <summary>
            /// Initializes a new instance of the <see cref="IncidentFormData"/> class.
            /// </summary>
            /// <param name="formData">The form data.</param>
            public ReturnToWorkWrapper(Dictionary<string, object> formData)
            {
                this.formData = formData;
            }
            
            /// <summary>
            /// Gets or sets a value indicating whether submitting return to work.
            /// </summary>
            public bool SubmittingReturnToWork
            {
                get => this.formData.GetValueAsBoolean("returnToWork.SubmittingReturnToWork");
                set => this.formData.UpsertValue("returnToWork.SubmittingReturnToWork", value);
            }

            /// <summary>
            /// Gets or sets a value indicating whether this instance can view lost days.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance can view lost days; otherwise, <c>false</c>.
            /// </value>
            public bool CanViewLostDays
            {
                get => this.formData.GetValueAsBoolean("returnToWork.canViewLostDays");
                set => this.formData.UpsertValue("returnToWork.canViewLostDays", value);
            }

            /// <summary>
            /// if there is an away from work returns true.
            /// </summary>
            public bool IsTimeAwayFromWork
            {
                get => this.formData.GetValueAsBoolean("returnToWork.isTimeAwayFromWork");
                set => this.formData.UpsertValue("returnToWork.isTimeAwayFromWork", value);
            }

            /// <summary>
            /// Gets or sets the away from work.
            /// </summary>
            public List<AwayFromWorkContent> AwayFromWorks
            {
                get => this.formData.GetValue<List<AwayFromWorkContent>>("returnToWork.timesAwayFromWork");
                set => this.formData.UpsertValue("returnToWork.timesAwayFromWork", value);
            }

            /// <summary>
            /// If there is a restriction returns true.
            /// </summary>
            public bool IsRestrictionAdded
            {
                get => this.formData.GetValueAsBoolean("returnToWork.restrictionsAdded");
                set => this.formData.UpsertValue("returnToWork.restrictionsAdded", value);
            }

            /// <summary>
            /// Gets or sets the work restrictions.
            /// Gets or sets a value indicating whether this instance can view claims details.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance can view claim details; otherwise, <c>false</c>.
            /// </value>
            public bool CanViewClaimsDetails
            {
                get => this.formData.GetValueAsBoolean("canViewClaimDetails");
                set => this.formData.UpsertValue("canViewClaimDetails", value);
            }

            /// <summary>
            /// Gets or sets the matrix.
            /// </summary>
            public List<WorkRestrictionContent> WorkRestrictions
            {
                get => this.formData.GetValue<List<WorkRestrictionContent>>("returnToWork.workRestrictions");
                set => this.formData.UpsertValue("returnToWork.workRestrictions", value);
            }

            /// <summary>
            /// Gets or sets the matrix.
            /// </summary>
            public List<JobTransferContent> JobTransferCycles
            {
                get => this.formData.GetValue<List<JobTransferContent>>("returnToWork.jobTransferCycles");
                set => this.formData.UpsertValue("returnToWork.jobTransferCycles", value);
            }

            public List<PhysicianVisitDates> PhysicianVisitDates
            {
                get => this.formData.GetValue<List<PhysicianVisitDates>>("returnToWork.physicianVisitsDates");
                set => this.formData.UpsertValue("returnToWork.physicianVisitsDates", value);
            }

            /// <summary>
            /// Gets or sets the matrix.
            /// </summary>
            public MatrixWrapper Matrix
            {
                get => new MatrixWrapper(this.formData);
            }
        }

        public class MatrixWrapper
        {
            /// <summary>
            /// The form data
            /// </summary>
            private readonly Dictionary<string, object> formData;


            /// <summary>
            /// Initializes a new instance of the <see cref="MatrixWrapper"/> class.
            /// </summary>
            /// <param name="formData">The form data.</param>
            public MatrixWrapper(Dictionary<string, object> formData)
            {
                this.formData = formData;
            }

            /// <summary>
            /// Gets or sets the lost days.
            /// </summary>
            public int LostDays
            {
                get => this.formData.GetValueAsInt("returnToWork.matrix.lostDays");
                set => this.formData.UpsertValue("returnToWork.matrix.lostDays", value);
            }

            /// <summary>
            /// Gets or sets the classification.
            /// </summary>
            public string Classification
            {
                get => this.formData.GetValueAsString("returnToWork.matrix.classification");
                set => this.formData.UpsertValue("returnToWork.matrix.classification", value);
            }

            /// <summary>
            /// Determines whether this instance is restriction.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is restriction; otherwise, <c>false</c>.
            /// </returns>
            public bool IsRestriction()
            {
                return this.Classification != null && this.Classification.Contains("restriction", StringComparison.CurrentCultureIgnoreCase);
            }

            /// <summary>
            /// Determines whether [is lost time].
            /// </summary>
            /// <returns>
            ///   <c>true</c> if [is lost time]; otherwise, <c>false</c>.
            /// </returns>
            public bool IsLostTime()
            {
                return this.Classification != null && this.Classification.Contains("losttime", StringComparison.CurrentCultureIgnoreCase);
            }
        }

        /// <summary>
        /// The name of injured profile.
        /// </summary>
        public class NameOfInjuredProfile
        {
            /// <summary>
            /// Gets or sets the first name.
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// Gets or sets the last name.
            /// </summary>
            public string LastName { get; set; }


            /// <summary>
            /// Gets or sets the Business Title.
            /// </summary>
            public string BusinessTitle { get; set; }

            /// <summary>
            /// Gets or sets the Id.
            /// </summary>
            public string Id { get; set; }
        }

        /// <summary>
        /// The name of injured wrapper.
        /// </summary>
        public class NameOfInjuredWrapper
        {
            /// <summary>
            /// Gets or sets the profile.
            /// </summary>
            public NameOfInjuredProfile Profile { get; set; }

            /// <summary>
            /// The to string.
            /// </summary>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            public override string ToString()
            {
                return $"{this.Profile.FirstName} {this.Profile.LastName}".TrimEnd();
            }
        }

        public class AwayFromWorkContent
        {
            public int CycleNo { get; set; }
            public int LostDays { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public DateTime? FollowUpVisitDate { get; set; }
            public string Id { get; set; }
        }

        public class WorkRestrictionContent
        {
            public int CycleNo { get; set; }
            public int LostDays { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public DateTime? FollowUpVisitDate { get; set; }
            public string Id { get; set; }
            public string Details { get; set; }
        }

        public class PhysicianVisitDates
        {
            public int Index { get; set; }
            public DateTime? Date { get; set; }
            public string Description { get; set; }
        }

        public class JobTransferContent
        {
            public int CycleNo { get; set; }
            public int LostDays { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool? IsPermanentJobTransfer { get; set; }
            public string Id { get; set; }
            public string Details { get; set; }
        }

        public class LocationDetailsContent
        {
            public string SiteId { get; set; }
            public string SiteAddress { get; set; }
            public string ContractId { get; set; }
            public string ContractName { get; set; }
            public string SiteName { get; set; }
            public IncidentClientScope IncidentHierarchyScope { get; set; }
            public List<IncidentLocationScope> Scopes { get; set; }
            public string ClientName { get; set; }
            public string SiteCode { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string AddressLine3 { get; set; }
            public string ZipCode { get; set; }
            public bool UserDefinedSite { get; set; }
            public string Coordinates { get; set; }
            public string CountryCode { get; set; }
        }

        public class IncidentClientScope
        {
            public string ClientCountryName { get; set; }
            public int ClientId { get; set; }
            public int CountryId { get; set; }
            public int ManagingOfficeId { get; set; }
            public string ManagingOfficeName { get; set; }
            public int BusinessSegmentId { get; set; }
            public int SubBusinessSegmentId { get; set; }
            public int RegionId { get; set; }
            public int DivisionId { get; set; }
            public string BusinessArea { get; set; }
            public string ClientHierarchy { get; set; }
        }

        public class IncidentLocationScope
        {
            public string EntityCode { get; set; }
            public int EntityId { get; set; }
            public string EntityName { get; set; }
            public int ScopeLevel { get; set; }
        }

        /// <summary>
        /// These are the states of the Incident.
        /// Will populate this list on Incident Creation and manage them based on the Incident Progress.
        /// </summary>
        public List<EntityState> States { get; set; } = new List<EntityState>();
    }
}
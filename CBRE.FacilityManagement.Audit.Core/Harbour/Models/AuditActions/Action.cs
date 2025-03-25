// <copyright file="Action.cs" company="CBRE">
// Copyright (c) CBRE. All rights reserved.
// </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.AuditActions
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using CBRE.FacilityManagement.Audit.Core.Harbour.Common;
    using Newtonsoft.Json;
    using CBRE.FacilityManagement.Audit.Core.Harbour.Models;
    using NodaTime;

    using Slapper;

    /// <summary>
    /// The action.
    /// </summary>
    [Collection("Action")]
    [Entity("Action")]
    public class Action: RelatedItem, IHasStates
    {
        private ActionFormData actionFormData; 

        /// <summary>
        /// Gets or sets form data
        /// </summary>
        public dynamic FormData { get; set; }

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        /// <value>
        /// The creator.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// These are the states of the Audit.
        /// Will populate this list on Audit Creation and manage them based on the Audit Progress.
        /// </summary>
        public List<EntityState> States { get; set; } = new List<EntityState>();

        /// <summary>
        /// Get action description.
        /// </summary>
        /// <returns>
        ///   <c>string</c> return action description.
        /// </returns>
        public string GetActionDescription()
        {
            var formData = this.CreateStronglyTypedFormData();
            return formData.Description;
        }

        /// <summary>
        /// Get action rootcause.
        /// </summary>
        /// <returns>
        ///   <c>string</c> return action rootcause.
        /// </returns>
        public string GetActionRootCause()
        {
            var formData = this.CreateStronglyTypedFormData();
            return formData.RootCause;
        }

        /// <summary>
        /// Get action due date.
        /// </summary>
        /// <returns>
        ///   <c>string</c> return action due date.
        /// </returns>
        public DateTime GetActionDueDate()
        {
            var formData = this.CreateStronglyTypedFormData();
            return formData.DueDate;
        }

        /// <summary>
        /// Get action category.
        /// </summary>
        /// <returns>
        ///   <c>string</c> return action Category.
        /// </returns>
        public string GetActionCategory()
        {
            var formData = this.CreateStronglyTypedFormData();
            return formData.ActionCategory;
        }


        /// <summary>
        /// Get Assignee's Email.
        /// </summary>
        /// <returns>
        ///   <c>string</c> return Assignee's Email.
        /// </returns>
        public string GetAssigneeResponsibleEmail()
        {
            var formData = this.CreateStronglyTypedFormData();
            return formData.AssigneeResponsibleEmail;
        }

        /// <summary>
        /// The create strongly typed form data.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionFormData"/>.
        /// </returns>
        public ActionFormData CreateStronglyTypedFormData()
        {
            if (this.actionFormData == null)
            {
                this.actionFormData = ActionFormData.From(this.FormData);
            }

            return this.actionFormData;
        }

        public class ActionFormData
        {
            /// <summary>
            /// The form data
            /// </summary>
            private readonly Dictionary<string, object> formData;

            public ActionFormData(List<KeyValuePair<string, object>> formData)
            {
                this.formData = new Dictionary<string, object>(formData, StringComparer.InvariantCultureIgnoreCase);
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

            public string Description
            {
                get => this.formData.GetValueAsString("action.Description");
                set => this.formData.UpsertValue("action.Description", value);
            }

            public string RootCause
            {
                get => this.formData.GetValueAsString("action.rootCause");
                set => this.formData.UpsertValue("action.rootCause", value);
            }

            public bool? CanEdit
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.canEdit", false);
                set => this.formData.UpsertValue("action.canEdit", value);
            }

            public DateTime DueDate
            {
                get => this.formData.GetValueAsDateTime("action.dueDate");
                set => this.formData.UpsertValue("action.dueDate", value);
            }

            public bool IsReminderSet
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.setReminder", false);
                set => this.formData.UpsertValue("action.setReminder", value);
            }

            public DateTime? ReminderDate
            {
                get => this.formData.GetValueAsDateTime("action.whenWouldYouLikeToBeReminded");
                set => this.formData.UpsertValue("action.whenWouldYouLikeToBeReminded", value);
            }

            public string Status
            {
                get => this.formData.GetValueAsString("action.status");
                set => this.formData.UpsertValue("action.status", value);
            }

            public string ActionCurrentStatus
            {
                get => this.formData.GetValueAsString("action.current.status");
                set => this.formData.UpsertValue("action.current.status", value);
            }

            public string isProjectRelated
            {
                get => this.formData.GetValueAsString("action.projectRelated");
                set => this.formData.UpsertValue("action.projectRelated", value);
            }

            public bool PendingPromotionApproval
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.pendingPromotionApproval", false);
                set => this.formData.UpsertValue("action.pendingPromotionApproval", value);
            }

            public bool? IsSupervisor
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.isSupervisor", false);
                set => this.formData.UpsertValue("action.isSupervisor", value);
            }

            public string PromotedByUserId
            {
                get => this.formData.GetValueAsString("action.promotedByUserId");
                set => this.formData.UpsertValue("action.promotedByUserId", value);
            }

            public string PromotedApprovedByUserId
            {
                get => this.formData.GetValueAsString("action.promotedApprovedByUserId");
                set => this.formData.UpsertValue("action.promotedApprovedByUserId", value);
            }

            public string RecordId
            {
                get => this.formData.GetValueAsString("recordId");
                set => this.formData.UpsertValue("recordId", value);
            }

            public bool? IsCreator
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.isCreator", false);
                set => this.formData.UpsertValue("action.isCreator", value);
            }

            public bool? IsAssignee
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.isAssignee", false);
                set => this.formData.UpsertValue("action.isAssignee", value);
            }

            public bool? IsVerifier
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.isVerifier", false);
                set => this.formData.UpsertValue("action.isVerifier", value);
            }

            public bool? IsAdmin
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.isAdmin", false);
                set => this.formData.UpsertValue("action.isAdmin", value);
            }

            public bool? IsAuditAction
            {
                get => this.formData.GetValueAsBooleanWithDefault("action.isAuditAction", false);
                set => this.formData.UpsertValue("action.isAuditAction", value);
            }


            /// <summary>
            /// Gets or sets the is deleted.
            /// </summary>
            /// <value>
            /// The isDeleted.
            /// </value>
            public bool? IsDeleted
            {
                get => this.formData.GetValueAsBooleanWithDefault("isDeleted", false);
                set => this.formData.UpsertValue("isDeleted", value);
            }

            /// <summary>
            /// Gets or sets the is deletion reason.
            /// </summary>
            /// <value>
            /// The reasonForDeletion.
            /// </value>
            public string ReasonForDeletion
            {
                get => this.formData.GetValueAsString("reasonForDeletion");
                set => this.formData.UpsertValue("reasonForDeletion", value);
            }

            /// <summary>
            /// Gets or sets the question title.
            /// </summary>
            /// <value>
            /// The questionTitle.
            /// </value>
            public string QuestionTitle
            {
                get => this.formData.GetValueAsString("action.questionTitle");
                set => this.formData.UpsertValue("action.questionTitle", value);
            }

            public ProfileWrapperWrapper AssigneeResponsible
            {
                get => this.formData.GetValue<ProfileWrapperWrapper>("action.assigneeResponsible");
                set => this.formData.UpsertValue("action.assigneeResponsible", value);
            }


             /// <summary>
            /// Gets the assignee responsible identifier.
            /// </summary>
            /// <value>
            /// The assignee responsible identifier.
            /// </value>
            public string AssigneeResponsibleId => this.AssigneeResponsible?.Profile?.Id;

            /// <summary>
            /// The assignee responsible email.
            /// </summary>
            public string AssigneeResponsibleEmail => this.AssigneeResponsible?.Profile?.Email;

            /// <summary>
            /// Gets or sets the verifier.
            /// </summary>
            public ProfileWrapperWrapper Verifier
            {
                get => this.formData.GetValue<ProfileWrapperWrapper>("action.verifier");
                set => this.formData.UpsertValue("action.verifier", value);
            }

            /// <summary>
            /// The verifier id.
            /// </summary>
            public string VerifierId => this.Verifier?.Profile?.Id;

            /// <summary>
            /// The verifier email.
            /// </summary>
            public string VerifierEmail => this.Verifier?.Profile?.Email;

            /// <summary>
            /// Gets or sets a value indicating whether verification required.
            /// </summary>
            public bool VerificationRequired
            {
                get => this.formData.GetValueAsBoolean("action.verificationRequired");
                set => this.formData.UpsertValue("action.verificationRequired", value);
            }

            /// <summary>
            /// Determines whether this instance has verifier.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance has verifier; otherwise, <c>false</c>.
            /// </returns>
            public bool HasVerifier()
            {
                return this.Verifier != null && !string.IsNullOrEmpty(this.Verifier.Profile.Id);
            }

            /// <summary>
            /// Determines whether [has assignee responsible].
            /// </summary>
            /// <returns>
            ///   <c>true</c> if [has assignee responsible]; otherwise, <c>false</c>.
            /// </returns>
            public bool HasAssigneeResponsible()
            {
                return this.AssigneeResponsible != null && !string.IsNullOrEmpty(this.AssigneeResponsible?.Profile?.Id);
            }

            /// <summary>
            /// Sets the resolved.
            /// </summary>
            public void SetResolved()
            {
                this.Status = "action.status.resolved";
            }

            /// <summary>
            /// Sets the in progress.
            /// </summary>
            public void SetInProgress()
            {
                this.Status = "action.status.in-progress";
            }

            /// <summary>
            /// Sets the promoted.
            /// </summary>
            public void SetPromoted()
            {
                this.Status = "action.status.promoted";
            }

            /// <summary>
            /// Sets the completed.
            /// </summary>
            public void SetCompleted()
            {
                this.Status = "action.status.completed";
            }

            /// <summary>
            /// Sets the completed.
            /// </summary>
            public void SetOverdue()
            {
                this.Status = "action.status.overdue";
            }
            /// <summary>
            /// Determines whether [is in progress].
            /// </summary>
            /// <returns>
            ///   <c>true</c> if [is in progress]; otherwise, <c>false</c>.
            /// </returns>
            public bool IsInProgress()
            {
                return this.Status != null && this.Status.Contains("in-progress", StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// Determines whether this instance is completed.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is completed; otherwise, <c>false</c>.
            /// </returns>
            public bool IsCompleted()
            {
                return this.Status != null && this.Status.Contains("completed", StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// Determines whether this instance is resolved.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is resolved; otherwise, <c>false</c>.
            /// </returns>
            public bool IsResolved()
            {
                return this.Status != null && this.Status.Contains("resolved", StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// Determines whether this instance is promoted.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is promoted; otherwise, <c>false</c>.
            /// </returns>
            public bool IsPromoted()
            {
                return this.Status != null && this.Status.Contains("promoted", StringComparison.InvariantCultureIgnoreCase);
            }

            /// <summary>
            /// Determines whether this instance is closed.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
            /// </returns>
            public bool IsClosed()
            {
                return this.PendingPromotionApproval || this.IsCompleted() || this.IsPromoted() || this.IsResolved();
            }

            /// <summary>
            /// Determines whether this instance is open.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is open; otherwise, <c>false</c>.
            /// </returns>
            public bool IsOpen()
            {
                return !this.IsClosed();
            }

          
            public DateTime VerificationDate
            {
                get => this.formData.GetValueAsDateTime("action.VerificationDate");
                set => this.formData.UpsertValue("action.VerificationDate", value);
            }

            /// <summary>
            /// Gets or Sets Action Actegory
            /// </summary>

            public string ActionCategory
            {
                get => this.formData.GetValueAsString("action.actionCategory");
                set => this.formData.UpsertValue("action.actionCategory", value);
            }

            public static ActionFormData From(dynamic formData)
            {
                var kvps =
                    JsonConvert.DeserializeObject<List<KeyValuePair<string, object>>>(formData.ToString());
                return new ActionFormData(kvps);
            }

            public dynamic ToDynamic()
            {
                return JsonConvert.DeserializeObject<dynamic>(
                    JsonConvert.SerializeObject(this.formData.ToList(), FormDataSerializer.JsonSerializerSettings));
            }

            /// <summary>
            /// Remove specific node of form data
            /// </summary>
            /// <param name="key"></param>
            public void RemoveKey(string key)
            {
                this.formData.Remove(key);
            }

            /// <summary>
            /// Find specific node of form data
            /// </summary>
            /// <param name="key"></param>
            public bool FindKey(string key)
            {
                return this.formData.ContainsKey(key);
            }

            public class ProfileWrapperWrapper
            {
                public ProfileWrapper Profile { get; set; }
            }

            public class ProfileWrapper 
            {
                public string Id { get; set; }

                public string FirstName { get; set; }

                public string LastName { get; set; }

                /// <summary>
                /// Gets or sets the email.
                /// </summary>
                public string Email { get; set; }
            }
        }
    }
}

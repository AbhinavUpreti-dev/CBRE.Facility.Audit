namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models
{
    using System;
    using System.Collections.Generic;

    public class EntityState
    {
        /// <summary>
        /// State Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// State name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// State Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Determines State Type : State or Composite.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Status start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Status end date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Check whether it is start date.
        /// </summary>
        public bool IsStartState { get; set; }

        /// <summary>
        /// Check whether it is end date.
        /// </summary>
        public bool IsEndState { get; set; }

        /// <summary>
        /// Determines whether the current state is Active or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Determines whether the fields are editable or not.
        /// </summary>
        public bool? IsEditable { get; set; }

        /// <summary>
        /// State Translation Key representing the user specific language.
        /// </summary>
        public string StateTranslationKey { get; set; }

        /// <summary>
        /// Gets or sets LastUpdatedByUserId.
        /// </summary>
        public string LastUpdatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets LastUpdatedOn.
        /// </summary>
        public DateTime LastUpdatedOn { get; set; } 

        /// <summary>
        /// Gets or sets the IsDeleted flag.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}

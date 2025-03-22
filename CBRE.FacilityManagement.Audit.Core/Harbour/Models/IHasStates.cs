namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models
{ 
    using System.Collections.Generic;

    public interface IHasStates
    {
        /// <summary>
        /// Gets or sets the States.
        /// </summary>
        List<EntityState> States { get; set; }
    }
}

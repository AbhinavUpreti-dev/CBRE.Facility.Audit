// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scope.cs" company="CBRE">
//   Copyright (c) CBRE. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums;
using System;
using System.Linq;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models
{
    /// <summary>
    /// Represents a scope item.
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// The entity id of the scope item.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The scope level of the item.
        /// </summary>
        public int ScopeLevelId { get; set; }

        /// <summary>
        /// Is this scope item in an ERP scope?
        /// </summary>
        public bool IsErpScope
        { 
            get
            {
                var vals = Enum.GetValues(typeof(ErpScopeLevel)).Cast<int>();
                return vals.Contains(this.ScopeLevelId);

            }
        }

        /// <summary>
        /// Is this scope item an HRIS scope?
        /// </summary>
        public bool IsHrisScope
        {
            get
            {
                var vals = Enum.GetValues(typeof(HrisScopeLevel)).Cast<int>();
                return vals.Contains(this.ScopeLevelId);
            }
        }

    }
}

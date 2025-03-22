using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Audit
{
    public class Selectable<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        public T Instance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is selected.
        /// </summary>
        public bool IsSelected { get; set; }
    }
}

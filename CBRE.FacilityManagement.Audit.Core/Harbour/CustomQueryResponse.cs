using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour
{
    public class CustomQueryResponse<T> : CustomQueryModel
        where T : class
    {
        public CustomQueryResponse(IEnumerable<T> responseData, string token)
        {
            this.Data = responseData;
            this.ContinuationToken = token;
        }

        /// <summary>
        /// Gets or sets data.
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }

    public class CustomQueryModel
    {
        /// <summary>
        /// Gets or sets continuation Token.
        /// </summary>
        public string ContinuationToken { get; set; }
    }
}

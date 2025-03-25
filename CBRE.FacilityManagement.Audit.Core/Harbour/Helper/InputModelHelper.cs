using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Helper
{
    public static class InputModelHelper
    {
        /// <summary>
        /// The get decoded filters.
        /// </summary>
        /// <param name="inputModel">
        /// The input model.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public static Dictionary<int, int> GetDecodedFilters(List<string> filters)
        {
            var decodedFilters = new Dictionary<int, int>();

            if (filters == null || filters.Count == 0)
            {
                return decodedFilters;
            }

            try
            {
                foreach (var filter in filters)
                {
                    var filterComponents = filter.Split('_');
                    int filterLevel = int.Parse(filterComponents[0]);
                    int filterValue = int.Parse(filterComponents[1]);
                    decodedFilters.Add(filterLevel, filterValue);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Exception occured while parsing filter values.", e);
            }

            return decodedFilters;
        }
    }
}

using CBRE.FacilityManagement.Audit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces
{
    public interface IELogBookRepository
    {
        Task<List<Customers>> GetCustomersAsync(int id = 0);

        // Task<List<Documents>> GetDocumentsAsync(Guid? entityId = null, int? documentGroupId = null, string name = null, string mimeType = null, string extension = null, bool? isActive = null);
    }
}

using CBRE.FacilityManagement.Audit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Contracts.Persistence
{
    public interface IELogBookRepository
    {
        Task<List<Customers>> GetCustomersAsync(int id=0);
        Task<List<Documents>> GetDocumentsAsync(Guid? entityId = null, int? documentGroupId = null, string name = null, string mimeType = null, string extension = null, bool? isActive = null);
    }
}

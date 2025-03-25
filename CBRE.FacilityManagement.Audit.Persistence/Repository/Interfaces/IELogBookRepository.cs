using CBRE.FacilityManagement.Audit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces
{
    public interface IELogBookRepository
    {
        Task<List<Customer>> GetCustomersAsync(int id = 0);
        Task<List<Documents>> GetDocumentsAsync(string customerName, string contractName, string buildingName, string documentGroup, string documentSubGroup);
        DocumentGroup GetDocumentGroupByName(string name);
    }
}

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
        Task<List<Customer>> GetCustomersAsync(int id=0);
        Task<Documents> GetDocumentsAsync(string customerName, string contractName, string buildingName);
    }
}

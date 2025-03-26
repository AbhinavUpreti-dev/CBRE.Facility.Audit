using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Action = CBRE.FacilityManagement.Audit.Core.ElogBook.Action;
using CBRE.FacilityManagement.Audit.Core.ElogBook;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces
{
    public interface IELogBookRepository
    {
        Task<List<Customer>> GetCustomersAsync(int id = 0);
        Task<List<Documents>> GetDocumentsAsync(string customerName, string contractName,
                                                      string buildingName, int documentGroup, int documentSubGroup ,string createdByUserId);
        DocumentGroup GetDocumentGroupByName(string name);
        Task<List<Action>> GetActionsByDocumentGroupIdAsync(int documentGroupId);
    }
}

using CBRE.FacilityManagement.Audit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Contracts.Persistence
{
    public interface IELogBookRepository
    {
        Task<List<Customers>> GetCustomersAsync(int id=0);
    }
}

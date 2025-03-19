using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository
{
    public class ELogBookRespository : IELogBookRepository
    {
        public async Task<List<Customers>> GetCustomersAsync(int id=0)
        {
            throw new NotImplementedException();
        }
    }
}

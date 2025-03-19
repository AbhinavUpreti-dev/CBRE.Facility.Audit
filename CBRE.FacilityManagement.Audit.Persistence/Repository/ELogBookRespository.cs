using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository
{
    public class ELogBookRespository : IELogBookRepository
    {
        protected readonly ELogBookDbContext _context;
        public ELogBookRespository(ELogBookDbContext eLogBookDbContext)
        {
            this._context = eLogBookDbContext;
        }
        public async Task<List<Customers>> GetCustomersAsync(int id=0)
        {
            return await _context.Customers.ToListAsync();
        }
    }
}

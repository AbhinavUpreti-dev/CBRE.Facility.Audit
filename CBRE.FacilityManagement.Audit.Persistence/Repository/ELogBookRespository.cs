using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Application.DTO;
using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Persistence.Repository
{
    public class ELogBookRepository : IELogBookRepository
    {
        protected readonly ELogBookDbContext _context;

        public ELogBookRepository(ELogBookDbContext eLogBookDbContext)
        {
            this._context = eLogBookDbContext;
        }

        public async Task<List<Customer>> GetCustomersAsync(int id = 0)
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Documents> GetDocumentsAsync(string customerName, string contractName, string buildingName)
        {
            var query = _context.Documents
                .Include(d => d.DocumentGroup)
                    .ThenInclude(dg => dg.DocumentGroupProperties)
                        .ThenInclude(dgp => dgp.ContractBuilding)
                            .ThenInclude(cb => cb.Contract)
                                .ThenInclude(c => c.Customer)
                .Include(d => d.DocumentGroup)
                    .ThenInclude(dg => dg.DocumentGroupProperties)
                        .ThenInclude(dgp => dgp.ContractBuilding)
                            .ThenInclude(cb => cb.Building)
                .AsQueryable();

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(d => d.DocumentGroup.DocumentGroupProperties
                    .Any(dgp => dgp.ContractBuilding.Contract.Customer.Name.Contains(customerName)));
            }

            if (!string.IsNullOrEmpty(contractName))
            {
                query = query.Where(d => d.DocumentGroup.DocumentGroupProperties
                    .Any(dgp => dgp.ContractBuilding.Contract.Name.Contains(contractName)));
            }

            if (!string.IsNullOrEmpty(buildingName))
            {
                query = query.Where(d => d.DocumentGroup.DocumentGroupProperties
                    .Any(dgp => dgp.ContractBuilding.Building.Name.Contains(buildingName)));
            }

            var documents = await query.Select(d => new Documents
            {
                FileData = d.FileData,
                Extension = d.Extension
            }).FirstOrDefaultAsync();

            return documents;
        }
    }
}

using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces;
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

        public async Task<List<Documents>> GetDocumentsAsync(string customerName, string contractName,
                                                            string buildingName, string documentGroup, string documentSubGroup)
        {
            var query = _context.Documents
                        .Include(d => d.DocumentGroup)
                            .ThenInclude(dg => dg.MasterDocumentGroup)
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
            //if (!string.IsNullOrEmpty(documentGroup))
            //{
            //    query = query.AsEnumerable().Where(d => d.DocumentGroup.EffectiveName.Contains(documentGroup) ||
            //                                            d.DocumentGroup.MasterDocumentGroup.Name.Contains(documentGroup)).AsQueryable();
            //}

            //if (!string.IsNullOrEmpty(documentSubGroup))
            //{
            //    query = query.AsEnumerable().Where(d => d.DocumentGroup.SubGroups.Any(sg => sg.Name.Contains(documentSubGroup) ||
            //                                                                                sg.MasterDocumentGroup.Name.Contains(documentSubGroup))).AsQueryable();
            //}



            var documents = await query.Select(d => new Documents
            {
                FileData = d.FileData,
                Extension = d.Extension,
                DocumentGroup = d.DocumentGroup,
            }).ToListAsync();

            return documents.Where(i => i.DocumentGroup.EffectiveName.Contains(documentSubGroup)).ToList();
        }

        public DocumentGroup GetDocumentGroupByName(string name)
        {
            return _context.DocumentGroups
                .Include(dg => dg.MasterDocumentGroup)
                .Where(dg => dg.Name == name || (dg.MasterDocumentGroup != null && dg.MasterDocumentGroup.Name == name))
                .FirstOrDefault();
        }


    }
}

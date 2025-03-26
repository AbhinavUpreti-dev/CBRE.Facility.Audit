using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Core.ElogBook;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using CBRE.FacilityManagement.Audit.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action = CBRE.FacilityManagement.Audit.Core.ElogBook.Action;

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
                                                 string buildingName, int documentGroupId, int documentSubGroupId, string createdByUserId)
        {
            var query = _context.Documents
                .AsNoTracking()
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
                    .Any(dgp => dgp.ContractBuilding.Building.Name.Equals(buildingName)));
            }

            if (documentGroupId > 0)
            {
                query = query.Where(d => d.DocumentGroupId == documentGroupId);
            }

            if (!string.IsNullOrEmpty(createdByUserId))
            {
                var userId = await _context.UserProfiles
                    .Where(up => up.UserId.Contains(createdByUserId))
                    .Select(up => up.Id)
                    .FirstOrDefaultAsync();

                query = query.Where(d => d.CreatedById == userId);
            }

            var documentList = await query
                .OrderByDescending(d => d.Created).Take(1)
                .Select(d => new
                {
                    d.Id,
                    d.FileData,
                    d.Extension,
                    d.DocumentGroupId,
                    ContractName = d.DocumentGroup.DocumentGroupProperties
                        .Select(dgp => dgp.ContractBuilding.Contract.Name).FirstOrDefault(),
                    CustomerName = d.DocumentGroup.DocumentGroupProperties
                        .Select(dgp => dgp.ContractBuilding.Contract.Customer.Name).FirstOrDefault(),
                    d.CreatedById,
                    ContractBuildingId = d.DocumentGroup.DocumentGroupProperties
                        .Select(dgp => dgp.ContractBuilding.Id).FirstOrDefault(),
                    BuildingName = d.DocumentGroup.DocumentGroupProperties
                        .Select(dgp => dgp.ContractBuilding.Building.Name).FirstOrDefault()
                })
                .ToListAsync();

            var documents = documentList.Select(d => new Documents
            {
                Id = d.Id,
                FileData = d.FileData,
                Extension = d.Extension,
                DocumentGroupId = d.DocumentGroupId,
            }).ToList();

            return documents;
        }


        public DocumentGroup GetDocumentGroupById(int Id)
        {
            return _context.DocumentGroups
                .Include(dg => dg.MasterDocumentGroup)
                .Where(dg => dg.Id == Id).FirstOrDefault();
        }
        private string GetDocumentGroupPair(int documentGroupID, out string documentGroupName)
        {
            documentGroupName = string.Empty;
            string documentSubGroupName = string.Empty;
            var documentGroup = this._context.DocumentGroups.Find(documentGroupID);
            if (documentGroup != null)
            {
                if (documentGroup.ParentId != null)
                {
                    var parentDocumentGroup = this._context.DocumentGroups.Find(documentGroup.ParentId);
                    if (string.IsNullOrEmpty(parentDocumentGroup.Name))
                        documentGroupName = this._context.MasterDocumentGroups.Find(parentDocumentGroup.MasterDocumentGroupId).Name;
                    else
                        documentGroupName = parentDocumentGroup.Name;
                    if (string.IsNullOrEmpty(documentGroup.Name))
                        documentSubGroupName = this._context.MasterDocumentGroups.Find(documentGroup.MasterDocumentGroupId).Name;
                    else
                        documentSubGroupName = documentGroup.Name;
                }
                else
                {
                    if (string.IsNullOrEmpty(documentGroup.Name))
                        documentGroupName = this._context.MasterDocumentGroups.Find(documentGroup.MasterDocumentGroupId).Name;
                    else
                        documentGroupName = documentGroup.Name;
                }
            }
            return documentSubGroupName;
        }


        public DocumentGroup GetDocumentGroupByName(string name)
        {
            return _context.DocumentGroups
                .Include(dg => dg.MasterDocumentGroup)
                .Where(dg => dg.Name == name || (dg.MasterDocumentGroup != null && dg.MasterDocumentGroup.Name == name))
                .FirstOrDefault();
        }
        public async Task<List<Action>> GetActionsByDocumentGroupIdAsync(int documentGroupId)
        {
            return await _context.Actions
                .Where(a => a.DocumentGroupId == documentGroupId)
                .Select(a => new Action
                {
                    Id = a.Id,
                    Description = a.Description,
                    Status = a.Status,
                    RequiredDate = a.RequiredDate,
                    ClosedDate = a.ClosedDate
                })
                .ToListAsync();
        }


    }
}

using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Core;
using CBRE.FacilityManagement.Audit.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public async Task<List<Documents>> GetDocumentsAsync(Guid? entityId = null, int? documentGroupId = null, string name = null, string mimeType = null, string extension = null, bool? isActive = null)
        {
            var query = _context.Documents.AsQueryable();

            if (entityId.HasValue)
            {
                query = query.Where(d => d.EntityId == entityId.Value);
            }

            if (documentGroupId.HasValue)
            {
                query = query.Where(d => d.DocumentGroupId == documentGroupId.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(mimeType))
            {
                query = query.Where(d => d.MimeType == mimeType);
            }

            if (!string.IsNullOrEmpty(extension))
            {
                query = query.Where(d => d.Extension == extension);
            }

            if (isActive.HasValue)
            {
                query = query.Where(d => d.IsActive == isActive.Value);
            }

            return await query.ToListAsync();
        }
    }
}

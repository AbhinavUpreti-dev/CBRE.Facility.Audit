using CBRE.FacilityManagement.Audit.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core
{
    public class Customers : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string UniqueId { get; set; }
        public bool IsActive { get; set; }
        public string Discriminator { get; set; }
        public DateTime LoadDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string GlobalCustomerName { get; set; }
        public string GlobalCustomerId { get; set; }
        public bool IsCMT { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNumber { get; set; }
        public string ContactPersonEmail { get; set; }
        public string? Notes { get; set; }
    }
}

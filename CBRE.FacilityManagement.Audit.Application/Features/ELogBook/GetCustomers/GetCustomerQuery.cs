using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers
{
    public record GetCustomerQuery : IRequest<List<CustomerDTO>>;

    
}

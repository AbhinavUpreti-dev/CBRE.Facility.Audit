using AutoMapper;
using CBRE.FacilityManagement.Audit.Application.Contracts.Persistence;
using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.Features.ELogBook.GetCustomers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, List<CustomerDTO>>
    {
        private readonly IELogBookRepository _elogBookRepository;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IELogBookRepository elogBookRepository, IMapper mapper)
        {
            _elogBookRepository = elogBookRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _elogBookRepository.GetCustomersAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }
    }
}

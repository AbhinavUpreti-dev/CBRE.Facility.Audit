using AutoMapper;
using CBRE.FacilityManagement.Audit.Application.DTO.Elogbook;
using CBRE.FacilityManagement.Audit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Application.MappingProfile
{
    public class ElogBookMappingProfile : Profile
    {
        public ElogBookMappingProfile()
        {
            CreateMap<Customers, CustomerDTO>();
        }
    }
}

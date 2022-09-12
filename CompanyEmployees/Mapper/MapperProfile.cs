using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace CompanyEmployees.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress, opt => opt.MapFrom(d => string.Join(' ', d.Address, d.Country)));

            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
        }
    }
}

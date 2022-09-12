using AutoMapper;
using Entities.DTO.CompanyDto;
using Entities.DTO.CompanyEmployeeDto;
using Entities.DTO.EmployeeDto;
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

            // createion
            CreateMap<CompanyCreationDto, Company>();
            CreateMap<CreateEmployeeDto, Employee>();
            //
            CreateMap<CompanyEmployeeCreationDto, Company>();
        }
    }
}

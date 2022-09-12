using AutoMapper;
using Contracts;
using Entities.DTO.CompanyDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployees.Controllers
{
    [Route("api/v1/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepoManager _manager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(ILoggerManager logger, IRepoManager manager, IMapper mapper)
        {
            _logger = logger;
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            

            var companies = _manager.Company.GetAllCompanies(trackChanges: false);
                var companiesDto = _mapper.Map <IEnumerable<CompanyDto>>(companies);

                /*var companiesDto = companies.Select(c => new CompanyDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    FullAddress = string.Join(" ", c.Address, c.Country)
                }).ToList();*/
                return Ok(companiesDto);
            

        }

        [HttpGet("{id}", Name ="getCompanyById")]
        public IActionResult GetCompanyById(Guid Id)
        {
            var company = _manager.Company.GetCompanyById(Id, trackChanges: false);
            if (company == null)
            {
                return NotFound(company);
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyCreationDto companyCreation)
        {
            if (companyCreation == null)
            {
                throw new ArgumentNullException("Null data provided");
            }


            // mapping: First object is the destination while the second item is the source
            var company = _mapper.Map<Company>(companyCreation);
            _manager.Company.CreateCompany(company);
            _manager.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(company);
            return CreatedAtRoute("getCompanyById", new { companyToReturn.Id }, companyToReturn);
        }
    }
}

using AutoMapper;
using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DTO.CompanyDto;
using Entities.DTO.CompanyEmployeeDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployees.Controllers
{
    [Route("api/v1/[controller]")]
    //[ApiController]
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

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            // mapping: First object is the destination while the second item is the source
            var company = _mapper.Map<Company>(companyCreation);
            _manager.Company.CreateCompany(company);
            _manager.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(company);
            return CreatedAtRoute("getCompanyById", new { companyToReturn.Id }, companyToReturn);
        }


        /// <summary>
        /// create both company and employee(s) at the same time
        /// </summary>
        /// <param name="companyEmployeeCreation"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost("company-employee")]
        public IActionResult CreateCompany([FromBody] CompanyEmployeeCreationDto companyEmployeeCreation)
        {
            if (companyEmployeeCreation == null)
            {
                throw new ArgumentNullException("Null data provided");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            // mapping: First object is the destination while the second item is the source
            var company = _mapper.Map<Company>(companyEmployeeCreation);
            _manager.Company.CreateCompany(company);
            _manager.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(company);
            return CreatedAtRoute("getCompanyById", new { companyToReturn.Id }, companyToReturn);
        }

        /// <summary>
        /// A method to get the collection of companies
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("collection/{ids}", Name = "companyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest($"Ids can not be null");
            }

            var companyCollection = _manager.Company.GetCompaniesById(ids, trackChanges: false);
            if (companyCollection.Count() != ids.Count())
            {
                return NotFound(companyCollection);
            }

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyCollection);
            return Ok(companyCollectionToReturn);
        }

        /// <summary>
        /// Endpoint to create a collection of companies
        /// </summary>
        /// <param name="companyCreationDtos"></param>
        /// <returns></returns>

        [HttpPost("collections")]
        public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyCreationDto> companyCreationDtos)
        {
            if(companyCreationDtos == null)
            {
                return BadRequest("Object can not be null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var companyCollection = _mapper.Map<IEnumerable<Company>>(companyCreationDtos);
            foreach(var company in companyCollection)
            {
                _manager.Company.CreateCompany(company);
            }

            _manager.Save();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyCollection);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("companyCollection", new { ids }, companyCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompanyById(Guid id)
        {
            var company = _manager.Company.GetCompanyById(id, trackChanges: false);
            if (company== null)
            {
                return NotFound($"Company with id: {id} not found");
            }

            _manager.Company.DeleteCompany(company);
            _manager.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(Guid id, [FromBody]CompanyUpdateDto companyUpdateDto)
        {
            if(companyUpdateDto == null)
            {
                return BadRequest($"No value passed");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var company = _manager.Company.GetCompanyById(id, trackChanges: true);
            if (company == null)
            {
                return NotFound($"Company with id: {id} not found ");
            }

            _mapper.Map(companyUpdateDto, company);
            _manager.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdateCompany(Guid id, [FromBody] JsonPatchDocument<CompanyUpdateDto> companyUpdate)
        {
            if (companyUpdate == null)
            {
                return BadRequest($"No value passed");
            }

            var company = _manager.Company.GetCompanyById(id, trackChanges: true);
            if (company == null)
            {
                return NotFound($"Company with id: {id} not found ");
            }

            var companyToPatch = _mapper.Map<CompanyUpdateDto>(company);
            companyUpdate.ApplyTo(companyToPatch, ModelState);

            TryValidateModel(companyToPatch);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(companyToPatch, company);
            _manager.Save();

            return NoContent();
        }
    }
}

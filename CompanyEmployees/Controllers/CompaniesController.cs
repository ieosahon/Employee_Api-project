using AutoMapper;
using CompanyEmployees.ActionFilters;
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
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllCompanies()
        {
            

            var companies = await _manager.Company.GetAllCompanies(trackChanges: false);
                var companiesDto = _mapper.Map <IEnumerable<CompanyDto>>(companies);
                return Ok(companiesDto);
            
        }

        [HttpGet("{id}", Name ="getCompanyById")]
        public async Task<IActionResult> GetCompanyById(Guid Id)
        {
            var company = await _manager.Company.GetCompanyById(Id, trackChanges: false);
            if (company == null)
            {
                return NotFound(company);
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationActionAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreationDto companyCreation)
        {

            // mapping: First object is the destination while the second item is the source
            var company = _mapper.Map<Company>(companyCreation);
            _manager.Company.CreateCompany(company);
            await _manager.SaveAsync();

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
        [ServiceFilter(typeof(ValidationActionAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyEmployeeCreationDto companyEmployeeCreation)
        {

            // mapping: First object is the destination while the second item is the source
            var company = _mapper.Map<Company>(companyEmployeeCreation);
            _manager.Company.CreateCompany(company);
            await _manager.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(company);
            return CreatedAtRoute("getCompanyById", new { companyToReturn.Id }, companyToReturn);
        }

        /// <summary>
        /// A method to get the collection of companies
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("collection/{ids}", Name = "companyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest($"Ids can not be null");
            }

            var companyCollection = await _manager.Company.GetCompaniesById(ids, trackChanges: false);
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

        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyCreationDto> companyCreationDtos)
        {

            var companyCollection = _mapper.Map<IEnumerable<Company>>(companyCreationDtos);
            foreach(var company in companyCollection)
            {
                _manager.Company.CreateCompany(company);
            }

            await _manager.SaveAsync();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyCollection);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("companyCollection", new { ids }, companyCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyById(Guid id)
        {
            var company =await _manager.Company.GetCompanyById(id, trackChanges: false);
            if (company== null)
            {
                return NotFound($"Company with id: {id} not found");
            }

            _manager.Company.DeleteCompany(company);
            await _manager.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationActionAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody]CompanyUpdateDto companyUpdateDto)
        {
            var company =await  _manager.Company.GetCompanyById(id, trackChanges: true);
            if (company == null)
            {
                return NotFound($"Company with id: {id} not found ");
            }

             _mapper.Map(companyUpdateDto, company);
            await _manager.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateCompany(Guid id, [FromBody] JsonPatchDocument<CompanyUpdateDto> companyUpdate)
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

            await _mapper.Map(companyToPatch, company);
            await _manager.SaveAsync();

            return NoContent();
        }
    }
}

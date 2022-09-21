using AutoMapper;
using CompanyEmployees.ActionFilters;
using Contracts;
using Entities.DTO.EmployeeDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/v1/companies/{companyId}/employees")]
    //[ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepoManager _manager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeesController(ILoggerManager logger, IRepoManager manager, IMapper mapper)
        {
            _logger = logger;
            _manager = manager;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(Guid companyId)
        {
            var company = await _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                return NotFound(company);
            }

            var employees = _manager.Employee.GetAllEmployee(companyId, trackChanges: false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(Guid companyId, Guid id)
        {
            var company =await _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                return NotFound(company);
            }

            var employee =await _manager.Employee.GetEmployeeById(companyId, id, trackChanges: false);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationActionAttribute))]
        public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] CreateEmployeeDto createEmployee)
        {

            var company =await _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                throw new ArgumentException($"Company with {companyId} not found");
            }

            var employee = _mapper.Map<Employee>(createEmployee);
            _manager.Employee.CreateEmployee(companyId, employee);
            await _manager.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
            return CreatedAtRoute("getEmployeeById", new { companyId, id = employeeToReturn.Id}, employeeToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid companyId, Guid id)
        {
            var company =await _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                return BadRequest($"company with id: {companyId} not found");
            }

            var employee = await _manager.Employee.GetEmployeeById(companyId, id, trackChanges: false);
            if (employee == null)
            {
                return NotFound();
            }

            _manager.Employee.DeleteEmployee(employee);
            await _manager.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationActionAttribute))]
        public async Task<IActionResult> UpdateEmployee(Guid companyId, Guid id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {

            var company =await _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                return NotFound($"Company with id: {companyId} not found");
            }

            var employee = _manager.Employee.GetEmployeeById(companyId, id, trackChanges: true);
            if (employee == null)
            {
                return NotFound($"Employee with id: {id} not found");
            }

            await _mapper.Map(employeeUpdateDto, employee);
            await _manager.SaveAsync();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialEmployeeUpdate(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeUpdateDto> employeeUpdateDto)
        {
            if (employeeUpdateDto == null)
            {
                return BadRequest("Object can not be null");
            }

            var company =await _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                return NotFound($"Company with id: {companyId} not found");
            }

            var employee = _manager.Employee.GetEmployeeById(companyId, id, trackChanges: true);
            if (employee == null)
            {
                return NotFound($"Employee with id: {id} not found");
            }

            var employeeToPatch = _mapper.Map<EmployeeUpdateDto>(employee);
            employeeUpdateDto.ApplyTo(employeeToPatch, ModelState);

            TryValidateModel(employeeToPatch);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _mapper.Map(employeeToPatch, employee);
            await _manager.SaveAsync();

            return NoContent();

        }
    }
}

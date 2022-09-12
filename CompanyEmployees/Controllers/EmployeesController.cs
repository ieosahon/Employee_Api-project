using AutoMapper;
using Contracts;
using Entities.DTO.EmployeeDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CompanyEmployees.Controllers
{
    [Route("api/v1/companies/{companyId}/employees")]
    [ApiController]
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
        public IActionResult GetAllEmployees(Guid companyId)
        {
            var company = _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                return NotFound(company);
            }

            var employees = _manager.Employee.GetAllEmployee(companyId, trackChanges: false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(Guid companyId, Guid id)
        {
            var company = _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                return NotFound(company);
            }

            var employee = _manager.Employee.GetEmployeeById(companyId, id, trackChanges: false);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]
        public IActionResult CreateEmployee(Guid companyId, [FromBody] CreateEmployeeDto createEmployee)
        {
            if (createEmployee == null)
            {
                throw new ArgumentNullException("Object is null");
            }

            var company = _manager.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                throw new ArgumentException($"Company with {companyId} not found");
            }

            var employee = _mapper.Map<Employee>(createEmployee);
            _manager.Employee.CreateEmployee(companyId, employee);
            _manager.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
            return CreatedAtRoute("getEmployeeById", new { companyId, id = employeeToReturn.Id}, employeeToReturn);
        }
    }
}

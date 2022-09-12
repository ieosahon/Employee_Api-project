using AutoMapper;
using Contracts;
using Entities.DTO;
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

            var employee = _manager.Employee.GetAllEmployee(companyId, trackChanges: false);
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employee);
            return Ok(employeeDto);
        }
    }
}

using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CompanyEmployees.Controllers
{
    [Route("api/v1/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepoManager _manager;
        private readonly ILoggerManager _logger;

        public CompaniesController(ILoggerManager logger, IRepoManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            try
            {
                var companies = _manager.Company.GetAllCompanies(trackChanges: false);

                var companiesDto = companies.Select(c => new CompanyDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    FullAddress = string.Join(" ", c.Address, c.Country)
                }).ToList();
                return Ok(companiesDto);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in {nameof(GetAllCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

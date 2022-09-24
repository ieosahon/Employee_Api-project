using AutoMapper;
using Contracts;
using Entities.DTO.CompanyDto;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/{version:apiVersion}/Companies")]
    [ApiController]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IRepoManager _repository;
        private readonly IMapper _mapper;

        public CompaniesV2Controller(IRepoManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        //[HttpHead]
        public async Task<IActionResult> GetAllCompanies([FromQuery] CompanyParameters companyParameters)
        {


            var companies = await _repository.Company.GetAllCompanies(companyParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination",
            JsonConvert.SerializeObject(companies.MetaData));


            //var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            //return Ok(companiesDto);
            return Ok(companies);

        }
    }
}

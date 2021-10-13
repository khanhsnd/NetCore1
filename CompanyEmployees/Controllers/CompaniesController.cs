using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult > GetCompanies()
        {
            var companies = await _repositoryManager.Company.GetAllCompanies(false);
            var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return Ok(companiesDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {   
            
            var company = await _repositoryManager.Company.GetCompany(id, false);
            if(company == null)
            {
                _loggerManager.LogError("Company not exist");
                return NotFound();
            }
            return Ok(company);
        }
    }
}

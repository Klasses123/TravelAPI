using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Core.Models;
using TravelAPI.Infrastructure.Interfaces;
using TravelAPI.ViewModels;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IMapper Mapper { get; }
        private IBaseRepository<Company> CompanyRepository { get; }
        
        public CompanyController(IMapper mapper, IBaseRepository<Company> companyRepository)
        {
            Mapper = mapper;
            CompanyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateCompanyRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(
                Mapper.Map<CompanyViewModel>(
                    await CompanyRepository.CreateAsync(
                        Mapper.Map<Company>(request))));
        }
    }
}

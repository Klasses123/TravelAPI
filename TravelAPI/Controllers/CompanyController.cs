using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TravelAPI.Core.Models;
using TravelAPI.Services.Interfaces;
using TravelAPI.ViewModels;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IMapper Mapper { get; }
        private ICompanyService CompanyService { get; }
        
        public CompanyController(IMapper mapper, ICompanyService companyService)
        {
            Mapper = mapper;
            CompanyService = companyService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> Post([FromBody] CreateCompanyRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(
                Mapper.Map<CompanyViewModel>(
                    await CompanyService.CreateCompanyAsync(
                        Mapper.Map<Company>(request))));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(Guid id)
        {
            return new JsonResult(
                Mapper.Map<CompanyViewModel>(
                    await CompanyService.GetCompanyByIdAsync(id)));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> Delete(Guid id)
        {
            return new JsonResult(
                await CompanyService.DeleteCompanyAsync(id));
        }

        [HttpPost("update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> Update([FromBody] UpdateCompanyRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
               
            return new JsonResult(
                Mapper.Map<CompanyViewModel>(
                    await CompanyService.UpdateCompanyAsync(
                        Mapper.Map<Company>(request))));
        }
    }
}

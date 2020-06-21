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
        private IUserService<User> UserService { get; }
        
        public CompanyController(IMapper mapper, ICompanyService companyService, IUserService<User> userService)
        {
            Mapper = mapper;
            CompanyService = companyService;
            UserService = userService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> Post([FromBody] CreateCompanyRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyToCreate = Mapper.Map<Company>(request);
            companyToCreate.Owner = new User { UserName = request.OwnerName };

            return new JsonResult(
                Mapper.Map<CompanyViewModel>(
                    await CompanyService.CreateCompanyAsync(companyToCreate)));
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

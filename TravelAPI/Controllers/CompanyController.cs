﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TravelAPI.Common.Exceptions.ClientExceptions;
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

        [HttpGet("getByName/{name}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> GetByName(string name)
        {
            return new JsonResult(
                Mapper.Map<CompanyViewModel>(
                    await CompanyService.GetCompanyByName(name)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(Guid id)
        {
            return new JsonResult(
                Mapper.Map<CompanyViewModel>(
                    await CompanyService.GetCompanyByIdAsync(id)));
        }

        [HttpDelete("delete/{name}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> Delete(string name)
        {
            if (!User.HasClaim(c => c.Value.Contains("Owner")))
                throw new AccessException("У Вас недостаточно прав!");

            return new JsonResult(
                await CompanyService.DeleteCompanyAsync(name));
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

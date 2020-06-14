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
    public class TravelController : ControllerBase
    {
        private IMapper Mapper { get; }
        private ITravelService TravelService { get; }
        public TravelController(IMapper mapper, ITravelService travelService)
        {
            Mapper = mapper;
            TravelService = travelService;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> Post([FromBody] CreateTravelRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(
                Mapper.Map<TravelViewModel>(
                    await TravelService.CreateTravelAsync(
                        Mapper.Map<Travel>(request))));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(Guid id)
        {
            return new JsonResult(
                Mapper.Map<TravelViewModel>(
                    await TravelService.GetTravelByIdAsync(id)));
        }

        [HttpPost("update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> Update([FromBody] UpdateTravelRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(
                Mapper.Map<TravelViewModel>(
                    await TravelService.UpdateTravel(
                        Mapper.Map<Travel>(request))));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> Delete(Guid id)
        {
            return new JsonResult(
                await TravelService.DeleteTravel(id));
        }
    }
}

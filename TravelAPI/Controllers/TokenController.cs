using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Core.Models;
using TravelAPI.Services.Interfaces;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Controllers
{
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IMapper Mapper { get; }
        private IUserService<User> UserService { get; }
        public TokenController(IMapper mapper, IUserService<User> userService)
        {
            Mapper = mapper;
            UserService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(await UserService.SignInAsync(request.UserName, request.Password));
        }
    }
}

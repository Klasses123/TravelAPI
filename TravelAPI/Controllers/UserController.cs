using AutoMapper;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Common.Exceptions.ClientExceptions;
using TravelAPI.Core.Models;
using TravelAPI.Infrastructure.Interfaces;
using TravelAPI.Services.Interfaces;
using TravelAPI.ViewModels.RequestModels;

namespace TravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService<User> UserService { get; }
        private IMapper Mapper { get; }
        //TODO: сделать обратный маппинг из User в UserViewModel, создать соответствующую ViewModel
        public UserController(IUserService<User> userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        [HttpPost("/user/create")]
        public async Task<ActionResult<string>> Post([FromBody] CreateUserRequest request)
        {
            if (request == null)
                throw new MissingParametersException(
                    new List<string> { $"Запрос на создание пользователя не может быть пустым" });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = Mapper.Map<CreateUserRequest, User>(request);
            return new JsonResult(await UserService.RegisterAsync(user, request.Password));
        }

        [HttpGet("/user/get/{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return new JsonResult(await UserService.GetUserAsync(id));
        }

        [HttpDelete("/user/delete/{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return new JsonResult(await UserService.DeleteUserAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> UpdateEmail([FromBody] UpdateEmailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(await UserService.UpdateEmailAsync(request));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(await UserService.UpdatePasswordAsync(request));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> UpdateName([FromBody] UpdateNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new JsonResult(await UserService.UpdateNameAsync(request));
        }
    }
}

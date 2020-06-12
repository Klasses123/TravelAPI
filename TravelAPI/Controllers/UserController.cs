using AutoMapper;
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

        [HttpGet("/user/get")]
        public async Task<ActionResult<string>> Get()
        {
            return await Task.Run(() => "abcd");
        }
    }
}

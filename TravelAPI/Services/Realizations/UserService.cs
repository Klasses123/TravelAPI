using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelAPI.Common.Exceptions.ClientExceptions;
using TravelAPI.Core.Models;
using TravelAPI.Infrastructure.Interfaces;
using TravelAPI.Infrastructure.Security;
using TravelAPI.Services.Interfaces;
using TravelAPI.ViewModels;
using TravelAPI.ViewModels.RequestModels;
using TravelAPI.ViewModels.ResponseModels;

namespace TravelAPI.Services.Realizations
{
    public class UserService : UserManager<User>, IUserService<User>
    {
        private IBaseRepository<User> UserRepository { get; }
        private SignInManager<User> SignInManager { get; }
        private IMapper Mapper { get; }
        public UserService(
            //For base class
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            //End for base class
            SignInManager<User> signInManager,
            IBaseRepository<User> userRepository,
            IMapper mapper)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            UserRepository = userRepository;
            SignInManager = signInManager;
            Mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("Пользователь не найден!");

            var result = await DeleteAsync(user);
            if (!result.Succeeded)
                throw new IdentityUserException(result);

            return result.Succeeded;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var user = await UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("Пользователь не найден!");

            return user;
        }

        public async Task<RegisterUserResponse> RegisterAsync(User user, string password)
        {
            var u = await UserRepository.GetFirstWhereAsync(u => u.UserName == user.UserName);
            if (u != null)
                throw new AlreadyExistsException("Пользователь", "логином");

            user.RegisteredOn = DateTime.Now;

            var result = await CreateAsync(user, password);
            if (!result.Succeeded)
                throw new IdentityUserException(result);

            return new RegisterUserResponse();
        }

        public async Task<bool> UpdateEmailAsync(UpdateEmailRequest request)
        {
            var user = await UserRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            var result = await ChangeEmailAsync(user, request.NewEmail, request.Token);
            if (!result.Succeeded)
                throw new IdentityUserException(result);

            return result.Succeeded;
        }

        public async Task<bool> UpdatePasswordAsync(UpdatePasswordRequest request)
        {
            var user = await UserRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new NotFoundException("Пользователь не найден!");

            var result = await ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
                throw new IdentityUserException(result);

            return result.Succeeded;
        }

        public async Task<User> UpdateNameAsync(UpdateNameRequest request)
        {
            var user = await UserRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            return await UserRepository.UpdateAsync(user);
        }

        public Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = UserRepository.GetAll(
                    u => u.UserName == userName) 
                .Include(u => u.Roles) 
                .Include(u => u.Company)
                .AsTracking()
                .FirstOrDefault();

            if (user == null)
                throw new NotFoundException("Пользователь с таким логином не найден!");

            return Task.FromResult(user);
        }

        public async Task<SignInResponse> SignInAsync(string userName, string password)
        {
            var result = await SignInManager.PasswordSignInAsync(userName, password, true, true);
            var user = UserRepository.GetAll(u => u.UserName == userName)
                .Include(u => u.Roles)
                    .ThenInclude(r => r.CompanyRole)
                .Include(u => u.Company)
                .AsTracking()
                .FirstOrDefault();
            
            if (!result.Succeeded || user == null)
                throw new SignInException();

            return await GenerateToken(user);
        }

        public async Task<RefreshTokenResponse> RefreshToken(string refreshToken)
        {
            var user = await UserRepository.GetFirstWhereAsync(u => u.RefreshToken == refreshToken);
            if (user == null)
                throw new NotFoundException("Пользователь не найден! Попробуйте перезайти в учетную запись!");

            var generateTokenResult = await GenerateToken(user);
            return new RefreshTokenResponse
            {
                Token = generateTokenResult.Token,
                RefreshToken = generateTokenResult.RefreshToken
            };
        }

        //private methods zone
        private async Task<SignInResponse> GenerateToken(User user)
        {
            var claims = GetIdentity(user);

            var response = new SignInResponse
            {
                RefreshToken = Guid.NewGuid().ToString(),
                Token = claims.GenerateJwtToken(),
                User = Mapper.Map<UserViewModel>(user)
            };
            user.RefreshToken = response.RefreshToken;
            await UserRepository.SaveAsync();

            return response;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var roleClaim = string.Join(',', user.Roles?.Select(r => r.CompanyRole.Name) ?? new List<string>());
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };
            if (!string.IsNullOrEmpty(roleClaim))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, roleClaim));
            }
            ClaimsIdentity claimsIdentity =
                   new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                       ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}

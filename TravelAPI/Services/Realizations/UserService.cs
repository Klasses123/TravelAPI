using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Common.Exceptions.ClientExceptions;
using TravelAPI.Core.Models;
using TravelAPI.Infrastructure.Interfaces;
using TravelAPI.Services.Interfaces;
using TravelAPI.ViewModels.RequestModels;
using TravelAPI.ViewModels.ResponseModels;

namespace TravelAPI.Services.Realizations
{
    public class UserService : UserManager<User>, IUserService<User>
    {
        private IBaseRepository<User> UserRepository { get; }
        private SignInManager<User> SignInManager { get; }
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
            IBaseRepository<User> userRepository)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            UserRepository = userRepository;
            SignInManager = signInManager;
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
            var u = UserRepository.GetAll(u => u.UserName == user.UserName).FirstOrDefault();
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
    }
}

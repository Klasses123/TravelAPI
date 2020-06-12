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

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterUserResponse> RegisterAsync(User user, string password)
        {
            //TODO: add user phone number and date of register
            var u = UserRepository.GetAll(u => u.UserName == user.UserName).FirstOrDefault();
            if (u != null)
                throw new AlreadyExistsException("Пользователь", "логином");

            var result = await CreateAsync(user, password);
            if (!result.Succeeded)
                throw new IdentityUserException(result);

            return new RegisterUserResponse();
        }
    }
}

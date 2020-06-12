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

namespace TravelAPI.Services.Realizations
{
    public class UserService : UserManager<User>, IUserService<User>
    {
        public IBaseRepository<User> UserRepository { get; }
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
            IBaseRepository<User> userRepository)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            UserRepository = userRepository;
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(User user, string password)
        {
            if (string.IsNullOrEmpty(user.Email) 
                || string.IsNullOrEmpty(user.Login)
                || string.IsNullOrEmpty(user.FirstName)
                || string.IsNullOrEmpty(user.LastName))
            {
                throw new MissingParametersException(new List<string>
                {
                    string.IsNullOrEmpty(user.Email) ? "E-mail" : "",
                    string.IsNullOrEmpty(user.Login) ? "Логин" : "",
                    string.IsNullOrEmpty(user.FirstName) ? "Имя" : "",
                    string.IsNullOrEmpty(user.LastName) ? "Фамилия" : ""
                }.Where(i => !string.IsNullOrEmpty(i)).ToList());
            }

            var u = UserRepository.GetAll(u => u.Login == user.Login).FirstOrDefault();
            if (u != null)
                throw new AlreadyExistsException("Пользователь", "логином");

            var result = await CreateAsync(user, password);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Core.Models;
using TravelAPI.Infrastructure.Interfaces;
using TravelAPI.Services.Interfaces;

namespace TravelAPI.Services.Realizations
{
    public class RoleService : IRoleService
    {
        private IBaseRepository<User> UserRepository { get; }
        public RoleService(IBaseRepository<User> userRepository)
        {
            UserRepository = userRepository;
        }

        public Task<bool> IsOwner(string userName)
        {
            var userRoles = UserRepository.GetAll(u => u.UserName == userName)
                .Include(u => u.Roles)
                    .ThenInclude(r => r.CompanyRole)
                .Select(u => u.Roles)
                .FirstOrDefault();

            return Task.FromResult(userRoles.Any(r => r.CompanyRole.Name == CompanyRole.OwnerRoleName));
        }

        public Task<bool> CanCreateTravel(string userName)
        {
            var user = UserRepository.GetAll(u => u.UserName == userName)
                .Include(u => u.Roles)
                    .ThenInclude(r => r.CompanyRole)
                .FirstOrDefault();

            return Task.FromResult(user.Roles.Any(r => r.CompanyRole.CanCreateTravel));
        }

    }
}

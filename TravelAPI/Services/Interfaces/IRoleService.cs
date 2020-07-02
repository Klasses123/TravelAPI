using System.Threading.Tasks;

namespace TravelAPI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> IsOwner(string userName);
        Task<bool> CanCreateTravel(string userName);
    }
}

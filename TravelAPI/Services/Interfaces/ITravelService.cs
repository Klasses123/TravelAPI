using System;
using System.Threading.Tasks;
using TravelAPI.Core.Models;

namespace TravelAPI.Services.Interfaces
{
    public interface ITravelService
    {
        Task<Travel> CreateTravelAsync(Travel travel);
        Task<Travel> GetTravelByIdAsync(Guid id);
        Task<Travel> UpdateTravel(Travel travel);
        Task<bool> DeleteTravel(Guid id);
    }
}

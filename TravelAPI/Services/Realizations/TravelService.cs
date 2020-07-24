using Microsoft.EntityFrameworkCore;
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
    public class TravelService : ITravelService
    {
        private IBaseRepository<Travel> TravelRepository { get; }
        private IBaseRepository<Company> CompanyRepository { get; }
        private IBaseRepository<User> UserRepository { get; }
        private IBaseRepository<Region> RegionRepository { get; }

        public TravelService(
            IBaseRepository<Travel> travelRepository, 
            IBaseRepository<Company> companyRepository,
            IBaseRepository<User> userRepository,
            IBaseRepository<Region> regionRepository)
        {
            TravelRepository = travelRepository;
            CompanyRepository = companyRepository;
            UserRepository = userRepository;
            RegionRepository = regionRepository;
        }

        public async Task<Travel> CreateTravelAsync(Travel travel)
        {
            var organizer = await CompanyRepository.GetByIdAsync(travel.CompanyOrganizer.Id);
            if (organizer == null)
                throw new NotFoundException("Компания не найдена!");

            var region = await RegionRepository.GetByIdAsync(travel.Region.Id);
            if (region == null)
                throw new NotFoundException("Регион не найден!");

            return await TravelRepository.CreateAsync(travel);
        }

        public Task<Travel> GetTravelByIdAsync(Guid id)
        {
            var requestedTravel = TravelRepository.GetAll(
                    t => t.Id == id) 
                .Include(t => t.Region)
                .Include(t => t.CompanyOrganizer).FirstOrDefault();

            if (requestedTravel == null)
                throw new NotFoundException("Путешествие не найдено!");

            return Task.FromResult(requestedTravel);
        }

        public async Task<Travel> UpdateTravel(Travel travel)
        {
            return await TravelRepository.UpdateAsync(travel);
        }

        public async Task<bool> DeleteTravel(Guid id)
        {
            var travelToDelete = await TravelRepository.GetByIdAsync(id);
            if (travelToDelete == null)
                throw new NotFoundException("Путешествие не найдено!");

            await TravelRepository.DeleteAsync(travelToDelete);

            return true;
        }
    }
}

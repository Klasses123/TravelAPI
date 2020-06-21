using System;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Common.Exceptions.ClientExceptions;
using TravelAPI.Core.Models;
using TravelAPI.Infrastructure.Interfaces;
using TravelAPI.Services.Interfaces;

namespace TravelAPI.Services.Realizations
{
    public class CompanyService : ICompanyService
    {
        private IBaseRepository<Company> CompanyRepository { get; }
        private IBaseRepository<User> UserRepository { get; }
        public CompanyService(IBaseRepository<Company> companyRepo, IBaseRepository<User> userRepository)
        {
            CompanyRepository = companyRepo;
            UserRepository = userRepository;
        }
        public async Task<Company> CreateCompanyAsync(Company company)
        {
            company.CreatedOn = DateTime.Now;
            company.Owner = (await UserRepository.GetAllAsync(
                u => u.UserName == company.Owner.UserName)).FirstOrDefault();

            var newCompany = await CompanyRepository.CreateAsync(company);
            await CompanyRepository.SaveAsync();
            return newCompany;
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            var companyToDelete = await CompanyRepository.GetByIdAsync(id);
            if (companyToDelete == null)
                throw new NotFoundException("Компания не найдена!");

            await CompanyRepository.DeleteAsync(companyToDelete);
            return true;
        }

        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            var company = await CompanyRepository.GetByIdAsync(id);
            if (company == null)
                throw new NotFoundException("Компания не найдена!");

            return company;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            return await CompanyRepository.UpdateAsync(company);
        }
    }
}

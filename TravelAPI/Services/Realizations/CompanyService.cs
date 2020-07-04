using Microsoft.AspNetCore.Mvc.Formatters.Xml;
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
    public class CompanyService : ICompanyService
    {
        private IBaseRepository<Company> CompanyRepository { get; }
        private IBaseRepository<User> UserRepository { get; }
        private IBaseRepository<CompanyRole> CompanyRolesRepository { get; }
        private IBaseRepository<UserCompanyRole> UserCompanyRolesRepository { get; }

        private readonly string _companyNotFound = "Компания не найдена!";

        public CompanyService(IBaseRepository<Company> companyRepo, 
            IBaseRepository<User> userRepository,
            IBaseRepository<CompanyRole> companyRolesRepository,
            IBaseRepository<UserCompanyRole> userCompanyRoleRepository)
        {
            CompanyRepository = companyRepo;
            UserRepository = userRepository;
            CompanyRolesRepository = companyRolesRepository;
            UserCompanyRolesRepository = userCompanyRoleRepository;
        }
        public async Task<Company> CreateCompanyAsync(Company company)
        {
            var exists = await CompanyRepository.GetFirstWhereAsync(c => c.Name == company.Name);
            if (exists != null)
                throw new AlreadyExistsException("Компания с таким названием уже существует!");

            company.CreatedOn = DateTime.Now;
            company.Owner = await UserRepository.GetFirstWhereAsync(
                u => u.UserName == company.Owner.UserName);
            company.Owner.Company = company;
            company.CompanyRoles = new List<CompanyRole> 
            { 
                CompanyRole.GetDefaultCompanyAdminRole(),
                CompanyRole.GetDefaultCompanyOwnerRole()
            };
            
            var newCompany = await CompanyRepository.CreateAsync(company);
            await CompanyRepository.SaveAsync();
            await UserRepository.UpdateAsync(newCompany.Owner);
            await UserRepository.SaveAsync();

            await UserCompanyRolesRepository.CreateAsync(
                new UserCompanyRole
                {
                    CompanyRole = company.CompanyRoles.FirstOrDefault(
                        r => r.Name == CompanyRole.OwnerRoleName),
                    User = company.Owner
                });
            await UserCompanyRolesRepository.SaveAsync();
            return newCompany;
        }

        public async Task<bool> DeleteCompanyAsync(string name)
        {
            var companyToDelete = await CompanyRepository.GetFirstWhereAsync(c => c.Name == name);
            if (companyToDelete == null)
                throw new NotFoundException(_companyNotFound);

            var companyUsers = UserRepository.GetAll(u => u.Company.Name == name).AsTracking();
            foreach (var u in companyUsers)
                u.Company = null;

            var companyRoles = CompanyRolesRepository.GetAll(r => r.Company.Name == name);
            await CompanyRolesRepository.DeleteRangeAsync(companyRoles);

            await CompanyRepository.DeleteAsync(companyToDelete);
            await CompanyRepository.SaveAsync();
            await UserRepository.SaveAsync();
            await CompanyRolesRepository.SaveAsync();
            return true;
        }

        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            var company = await CompanyRepository.GetByIdAsync(id);
            if (company == null)
                throw new NotFoundException(_companyNotFound);

            return company;
        }

        public async Task<Company> GetCompanyByName(string name)
        {
            var company = await CompanyRepository.GetFirstWhereAsync(c => c.Name == name);
            if (company == null)
                throw new NotFoundException(_companyNotFound);

            return company;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            return await CompanyRepository.UpdateAsync(company);
        }
    }
}

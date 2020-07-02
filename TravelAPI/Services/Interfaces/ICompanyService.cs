using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Core.Models;

namespace TravelAPI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> GetCompanyByIdAsync(Guid id);
        Task<bool> DeleteCompanyAsync(string name);
        Task<Company> UpdateCompanyAsync(Company company);
        Task<Company> GetCompanyByName(string name);
    }
}

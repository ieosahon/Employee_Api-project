using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepo
    {
        Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);
        Task<Company> GetCompanyById(Guid id, bool trackChanges);

        void CreateCompany(Company company);
        void DeleteCompany(Company company);

        // collections
        Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<Guid> ids, bool trackChanges);


    }
}

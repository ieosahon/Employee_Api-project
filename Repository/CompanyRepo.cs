using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepo : RepoBase<Company>, ICompanyRepo
    {
        public CompanyRepo(RepoContext repoContext) : base(repoContext)
        {

        }

        public void CreateCompany(Company company) => Create(company);

        public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Company> GetCompanyById(Guid id, bool trackChanges)
        {
            var company = await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
            if (company == null)
            {
                return new Company
                {
                    Id = id,
                    Name = $"Company with id: {id} not found "
                };
            }
            return company;
        }

        public async Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(c => ids.Contains(c.Id), trackChanges).ToListAsync();

        public void DeleteCompany(Company company) => Delete(company);
    }
}

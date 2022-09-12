using Contracts;
using Entities;
using Entities.Models;
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

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

        public Company GetCompanyById(Guid id, bool trackChanges)
        {
            var company = FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefault();
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
            
       
    }
}

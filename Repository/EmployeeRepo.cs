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
    public class EmployeeRepo : RepoBase<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(RepoContext repoContext) : base(repoContext)
        {

        }

        public IEnumerable<Employee> GetAllEmployee(Guid companyId, bool trackChanges)
        {
            var employeees = FindByCondition(e => e.CompanyId == companyId, trackChanges).OrderBy(e => e.Name);
            if (employeees == null)
            {
                return null;
            }

            return employeees.ToList();
        }

        public Employee GetEmployeeById(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}

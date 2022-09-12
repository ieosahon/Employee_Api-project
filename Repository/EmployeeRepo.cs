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

        public Employee GetEmployeeById(Guid companyId, Guid id, bool trackChanges)
        {
            var employee = FindByCondition(e => e.CompanyId == companyId && e.Id == id, trackChanges)
                .SingleOrDefault();
            if (employee == null)
            {
                return new Employee()
                {
                    Id = id,
                    Name = $"Employee with Id: {id} is not found"
                };
            }

            return employee;
        }
    }
}

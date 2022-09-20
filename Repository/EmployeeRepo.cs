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
    public class EmployeeRepo : RepoBase<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(RepoContext repoContext) : base(repoContext)
        {

        }

        public void CreateEmployee(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee(Guid companyId, bool trackChanges)
        {
            var employeees =await FindByCondition(e => e.CompanyId == companyId, trackChanges).OrderBy(e => e.Name).ToListAsync();
            if (employeees == null)
            {
                return null;
            }

            return employeees;
        }

        public async Task<Employee> GetEmployeeById(Guid companyId, Guid id, bool trackChanges)
        {
            var employee =await FindByCondition(e => e.CompanyId == companyId && e.Id == id, trackChanges)
                .SingleOrDefaultAsync();
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

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}

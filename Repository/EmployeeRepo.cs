using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryExtension;
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

        public async Task<PagedList<Employee>> GetAllEmployeeAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges) 
        {
            var employees = await FindByCondition(e => e.CompanyId == companyId, trackChanges)
                .FilterEmployee(employeeParameters.MinAge, employeeParameters.MaxAge)
                .SearchEmployee(employeeParameters.SearchTerm)
                .Sort(employeeParameters.OrderBy)
                .OrderBy(e => e.Name)
                .ToListAsync();
                
            if (employees == null)
            {
                return null;
            }

            return PagedList<Employee>
                .ToPagedList(employees, employeeParameters.PageNumber ,employeeParameters.PageSize);
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid companyId, Guid id, bool trackChanges)
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

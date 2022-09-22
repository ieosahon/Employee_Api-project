using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepo
    {
        Task<PagedList<Employee>> GetAllEmployeeAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployeeByIdAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployee(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}

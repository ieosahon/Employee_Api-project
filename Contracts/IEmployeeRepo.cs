using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepo
    {
        IEnumerable<Employee> GetAllEmployee(Guid companyId, bool trackChanges);
        Employee GetEmployeeById(Guid id, bool trackChanges);
    }
}

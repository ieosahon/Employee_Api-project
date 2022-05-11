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
    public class EmployeeRepo : RepoBase<CompanyEmployee>, IEmployeeRepo
    {
        public EmployeeRepo(RepoContext repoContext) : base(repoContext)
        {

        }
    }
}

using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepoManager : IRepoManager
    {
        private RepoContext _repoContext;
        private ICompanyRepo _companyRepo;
        private IEmployeeRepo _employeeRepo;

        public RepoManager(RepoContext repoContext)//, IEmployeeRepo employeeRepo, ICompanyRepo companyRepo)
        {
            _repoContext = repoContext;
            /*_employeeRepo = employeeRepo;
            _companyRepo = companyRepo;*/
        }

        public ICompanyRepo Company
        {
            get
            {
                if (_companyRepo == null)
                {
                    _companyRepo = new CompanyRepo(_repoContext);
                }
                return _companyRepo;
            }
        }

        public IEmployeeRepo Employee
        {
            get
            {
                if (_employeeRepo == null)
                {
                    _employeeRepo = new EmployeeRepo(_repoContext);
                }
                return _employeeRepo;
            }
        }


        public async Task SaveAsync() => await  _repoContext.SaveChangesAsync();


    }
}

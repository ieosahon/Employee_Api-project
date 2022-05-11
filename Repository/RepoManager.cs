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
        private readonly RepoContext _repoContext;
        private readonly ICompanyRepo _companyRepo;
        private readonly IEmployeeRepo _employeeRepo;

        public RepoManager(RepoContext repoContext, IEmployeeRepo employeeRepo, ICompanyRepo companyRepo)
        {
            _repoContext = repoContext;
            _employeeRepo = employeeRepo;
            _companyRepo = companyRepo;
        }

        public ICompanyRepo Company => throw new NotImplementedException();

        public IEmployeeRepo Employee => throw new NotImplementedException();

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}

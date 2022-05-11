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

        public RepoManager(RepoContext repoContext)
        {
            _repoContext = repoContext;
        }

        public ICompanyRepo Company => throw new NotImplementedException();

        public IEmployeeRepo Employee => throw new NotImplementedException();

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}

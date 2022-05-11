﻿using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepo : RepoBase<Company>, ICompanyRepo
    {
        public CompanyRepo(RepoContext repoContext) : base(repoContext)
        {

        }
    }
}

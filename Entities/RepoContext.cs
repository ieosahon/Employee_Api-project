using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities
{
    public class RepoContext : DbContext
    {
        public RepoContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
    }
}

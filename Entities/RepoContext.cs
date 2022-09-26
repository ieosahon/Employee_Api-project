using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.Repo;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Entities
{
    public class RepoContext : IdentityDbContext<User>
    {
        public RepoContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // for identity db context
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CompanyData());
            modelBuilder.ApplyConfiguration(new CompanyEmployeeRepo());
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> CompanyEmployees { get; set; }
    }
}

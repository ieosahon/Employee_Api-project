using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repo
{
    internal class CompanyEmployeeRepo : IEntityTypeConfiguration<CompanyEmployee>
    {
        public void Configure(EntityTypeBuilder<CompanyEmployee> builder)
        {
            builder.HasData
                (
                    new CompanyEmployee
                    {
                        Id = new Guid("6b6d4f35-5ad2-4efe-9365-7f2fb20ce840"),
                        Name = "Ofure Lawrence",
                        Age = 30,
                        Position = "Senior DotNet Developer",
                        CompanyId = new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa")
                    },

                    new CompanyEmployee
                    {
                        Id = new Guid("40d5671d-c386-40bd-a50b-8a784748259d"),
                        Name = "Emmanuel Ehimika",
                        Age = 32,
                        Position = "CEO",
                        CompanyId = new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2")
                    },

                    new CompanyEmployee
                    {
                        Id = new Guid("a0d3a936-90d1-4af3-8ab4-7d109b3d72f7"),
                        Name = "Darlington Kings",
                        Age = 25,
                        Position = "CTO",
                        CompanyId = new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa")
                    },

                    new CompanyEmployee
                    {
                        Id = new Guid("a92f7282-0784-47e9-92d4-462bda60f577"),
                        Name = "Osaro Obazee ",
                        Age = 27,
                        Position = "Manager",
                        CompanyId = new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2")
                    }
                );
        }
    }
}

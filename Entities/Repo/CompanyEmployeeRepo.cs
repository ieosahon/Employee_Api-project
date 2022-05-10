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
                        Id = new Guid("B0F20A77-06DA-4E80-A332-0688322522A6"),
                        Name = "Ofure Lawrence",
                        Age = 30,
                        Position = "Senior DotNet Developer",
                        CompanyId = new Guid("8C11C009-A3FD-4BF3-9E5A-1E9DAE4D3F32")
                    },

                    new CompanyEmployee
                    {
                        Id = new Guid("99B8EF16-4F5B-44F0-82D7-3EBFA5627312"),
                        Name = "Emmanuel Ehimika",
                        Age = 32,
                        Position = "CEO",
                        CompanyId = new Guid("EF0812D9-8107-4304-8CE6-13B62F628AC3")
                    },

                    new CompanyEmployee
                    {
                        Id = new Guid("9CD8BEF0 - A2BA - 4762 - B3CB - C65537E44EF8"),
                        Name = "Darlington Kings",
                        Age = 25,
                        Position = "CTO",
                        CompanyId = new Guid("8C11C009-A3FD-4BF3-9E5A-1E9DAE4D3F32")
                    },

                    new CompanyEmployee
                    {
                        Id = new Guid("8C3FC9CF-8365-432E-8424-B4C2F9D1B2EC"),
                        Name = "Osaro Obazee ",
                        Age = 27,
                        Position = "Manager",
                        CompanyId = new Guid("EF0812D9-8107-4304-8CE6-13B62F628AC3")
                    }
                );
        }
    }
}

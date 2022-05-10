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
    public class CompanyRepo : IEntityTypeConfiguration<Company>
    {

        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
                (
                    new Company
                    {
                        Id = new Guid("8C11C009-A3FD-4BF3-9E5A-1E9DAE4D3F32"),
                        Name = "Lehinet Solutions",
                        Address = "Edo Innovation hub, Benin City",
                        Country = "Nigeria"
                    },
                    new Company
                    {
                        Id = new Guid("EF0812D9-8107-4304-8CE6-13B62F628AC3"),
                        Name = "EThree Africa",
                        Address = " Edo Innovation hub, Benin City",
                        Country = "Nigeria"
                    }
                );
        }
    }
}

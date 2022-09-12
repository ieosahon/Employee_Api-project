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
    public class CompanyData : IEntityTypeConfiguration<Company>
    {

        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
                (
                    new Company
                    {
                        Id = new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"),
                        Name = "Lehinet Solutions",
                        Address = "Edo Innovation hub, Benin City",
                        Country = "Nigeria"
                    },
                    new Company
                    {
                        Id = new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"),
                        Name = "EThree Africa",
                        Address = " Edo Innovation hub, Benin City",
                        Country = "Nigeria"
                    }
                );
        }
    }
}

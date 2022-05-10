using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyEmployees.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"), "Edo Innovation hub, Benin City", "Nigeria", "Lehinet Solutions" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"), " Edo Innovation hub, Benin City", "Nigeria", "EThree Africa" });

            migrationBuilder.InsertData(
                table: "CompanyEmployees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("6b6d4f35-5ad2-4efe-9365-7f2fb20ce840"), 30, new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"), "Ofure Lawrence", "Senior DotNet Developer" },
                    { new Guid("a0d3a936-90d1-4af3-8ab4-7d109b3d72f7"), 25, new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"), "Darlington Kings", "CTO" },
                    { new Guid("40d5671d-c386-40bd-a50b-8a784748259d"), 32, new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"), "Emmanuel Ehimika", "CEO" },
                    { new Guid("a92f7282-0784-47e9-92d4-462bda60f577"), 27, new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"), "Osaro Obazee ", "Manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CompanyEmployees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("40d5671d-c386-40bd-a50b-8a784748259d"));

            migrationBuilder.DeleteData(
                table: "CompanyEmployees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("6b6d4f35-5ad2-4efe-9365-7f2fb20ce840"));

            migrationBuilder.DeleteData(
                table: "CompanyEmployees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("a0d3a936-90d1-4af3-8ab4-7d109b3d72f7"));

            migrationBuilder.DeleteData(
                table: "CompanyEmployees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("a92f7282-0784-47e9-92d4-462bda60f577"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"));
        }
    }
}

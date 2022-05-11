﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyEmployees.Migrations
{
    [DbContext(typeof(RepoContext))]
    [Migration("20220510054752_seedData")]
    partial class seedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"),
                            Address = "Edo Innovation hub, Benin City",
                            Country = "Nigeria",
                            Name = "Lehinet Solutions"
                        },
                        new
                        {
                            Id = new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"),
                            Address = " Edo Innovation hub, Benin City",
                            Country = "Nigeria",
                            Name = "EThree Africa"
                        });
                });

            modelBuilder.Entity("Entities.Models.CompanyEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EmployeeId");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyEmployees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6b6d4f35-5ad2-4efe-9365-7f2fb20ce840"),
                            Age = 30,
                            CompanyId = new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"),
                            Name = "Ofure Lawrence",
                            Position = "Senior DotNet Developer"
                        },
                        new
                        {
                            Id = new Guid("40d5671d-c386-40bd-a50b-8a784748259d"),
                            Age = 32,
                            CompanyId = new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"),
                            Name = "Emmanuel Ehimika",
                            Position = "CEO"
                        },
                        new
                        {
                            Id = new Guid("a0d3a936-90d1-4af3-8ab4-7d109b3d72f7"),
                            Age = 25,
                            CompanyId = new Guid("714c4c9c-d975-47ba-a2e1-396bf68c14aa"),
                            Name = "Darlington Kings",
                            Position = "CTO"
                        },
                        new
                        {
                            Id = new Guid("a92f7282-0784-47e9-92d4-462bda60f577"),
                            Age = 27,
                            CompanyId = new Guid("c1d1c92d-23ca-4c06-af26-ade1b8d76bb2"),
                            Name = "Osaro Obazee ",
                            Position = "Manager"
                        });
                });

            modelBuilder.Entity("Entities.Models.CompanyEmployee", b =>
                {
                    b.HasOne("Entities.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Entities.Models.Company", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
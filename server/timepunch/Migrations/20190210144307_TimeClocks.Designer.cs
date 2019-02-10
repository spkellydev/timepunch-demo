﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using timepunch.Models;

namespace timepunch.Migrations
{
    [DbContext(typeof(TimepunchContext))]
    [Migration("20190210144307_TimeClocks")]
    partial class TimeClocks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("timepunch.Models.CompanyModel", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyContact");

                    b.Property<string>("CompanyDescription");

                    b.Property<string>("CompanyLocation");

                    b.Property<string>("CompanyName");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("timepunch.Models.EmployeeModel", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompanyId");

                    b.Property<string>("EmployeePosition");

                    b.Property<double>("EmployeeSalary");

                    b.Property<int?>("ShiftId");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ShiftId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("timepunch.Models.ShiftModel", b =>
                {
                    b.Property<int>("ShiftId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AccruedHours");

                    b.Property<double>("AllowedHours");

                    b.Property<bool>("OvertimeAllowed");

                    b.Property<int>("PayPeriodDuration");

                    b.HasKey("ShiftId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("timepunch.Models.UserModel", b =>
                {
                    b.Property<string>("username")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("password");

                    b.HasKey("username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("timepunch.Models.UserProfileModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EmployeeId");

                    b.Property<string>("username");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("username");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("timepunch.Models.EmployeeModel", b =>
                {
                    b.HasOne("timepunch.Models.CompanyModel", "EmployeeCompany")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("timepunch.Models.ShiftModel", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId");
                });

            modelBuilder.Entity("timepunch.Models.UserProfileModel", b =>
                {
                    b.HasOne("timepunch.Models.EmployeeModel", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("timepunch.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("username");
                });
#pragma warning restore 612, 618
        }
    }
}

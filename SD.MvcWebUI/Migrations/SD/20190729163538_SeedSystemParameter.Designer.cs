﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SD.Entities;

namespace SD.MvcWebUI.Migrations.SD
{
    [DbContext(typeof(SDContext))]
    [Migration("20190729163538_SeedSystemParameter")]
    partial class SeedSystemParameter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SD.Entities.Concrete.SystemParameter", b =>
                {
                    b.Property<int>("ParameterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsReadOnly");

                    b.Property<string>("ParameterName");

                    b.Property<string>("ParameterValue");

                    b.HasKey("ParameterId");

                    b.ToTable("SystemParameters");

                    b.HasData(
                        new
                        {
                            ParameterId = 1,
                            Description = "Sistemin açık olup olmadığı durumu",
                            IsReadOnly = true,
                            ParameterName = "SYSTEMSTATUS",
                            ParameterValue = "ACTIVE"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

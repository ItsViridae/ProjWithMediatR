﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectZ.Data;

namespace ProjectZ.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190909150255_SeedingTesting")]
    partial class SeedingTesting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectZ.Data.Entities.Association", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Association");

                    b.HasData(
                        new
                        {
                            Id = 7,
                            Name = "Seven"
                        });
                });

            modelBuilder.Entity("ProjectZ.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .HasMaxLength(25);

                    b.Property<string>("LastName")
                        .HasMaxLength(25);

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin.com",
                            FirstName = "Seeded-FirstName",
                            LastName = "Seeded-LastName",
                            PasswordHash = new byte[] { 171, 19, 244, 249, 114, 132, 58, 64, 140, 25, 188, 167, 67, 179, 245, 157, 235, 123, 44, 50 },
                            PasswordSalt = new byte[] { 185, 165, 185, 133, 19, 43, 137, 79, 77, 68, 216, 129, 228, 70, 161, 229 }
                        });
                });

            modelBuilder.Entity("ProjectZ.Data.Entities.UserAssociation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssociationId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AssociationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAssociation");
                });

            modelBuilder.Entity("ProjectZ.Data.Entities.UserAssociation", b =>
                {
                    b.HasOne("ProjectZ.Data.Entities.Association", "Association")
                        .WithMany("UserAssociations")
                        .HasForeignKey("AssociationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectZ.Data.Entities.User", "User")
                        .WithMany("UserAssociations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
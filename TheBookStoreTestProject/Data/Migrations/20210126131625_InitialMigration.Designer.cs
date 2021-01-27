﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheBookStoreTestProject.Data;

namespace TheBookStoreTestProject.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20210126131625_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("TheBookStoreTestProject.Data.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Firstname = "Author1",
                            Lastname = "Author1"
                        },
                        new
                        {
                            Id = 2,
                            Firstname = "Author2",
                            Lastname = "Author2"
                        });
                });

            modelBuilder.Entity("TheBookStoreTestProject.Data.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            ISBN = "12345678910111",
                            Title = "Book 1"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            ISBN = "12345678910112",
                            Title = "Book 2"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 1,
                            ISBN = "12345678910113",
                            Title = "Book 3"
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 2,
                            ISBN = "12345678910114",
                            Title = "Book 4"
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 2,
                            ISBN = "12345678910115",
                            Title = "Book 5"
                        });
                });

            modelBuilder.Entity("TheBookStoreTestProject.Data.Models.Book", b =>
                {
                    b.HasOne("TheBookStoreTestProject.Data.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("TheBookStoreTestProject.Data.Models.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}

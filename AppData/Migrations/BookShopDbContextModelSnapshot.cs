﻿// <auto-generated />
using System;
using AppData.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppData.Migrations
{
    [DbContext(typeof(BookShopDbContext))]
    partial class BookShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppData.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("authors");
                });

            modelBuilder.Entity("AppData.Models.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BillCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("bills");
                });

            modelBuilder.Entity("AppData.Models.BillDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("BookId");

                    b.ToTable("billsDetails");
                });

            modelBuilder.Entity("AppData.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumberOfPurchase")
                        .HasColumnType("int");

                    b.Property<DateTime>("OpeningDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("books");
                });

            modelBuilder.Entity("AppData.Models.Cart", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.ToTable("carts");
                });

            modelBuilder.Entity("AppData.Models.CartDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("cartsDetails");
                });

            modelBuilder.Entity("AppData.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("AppData.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("AppData.Models.UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("RoleId");

                    b.ToTable("userRoles");
                });

            modelBuilder.Entity("AppData.Models.Bill", b =>
                {
                    b.HasOne("AppData.Models.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AppData.Models.BillDetail", b =>
                {
                    b.HasOne("AppData.Models.Bill", "Bill")
                        .WithMany("Details")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Book", "Book")
                        .WithMany("BillDetails")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("AppData.Models.Book", b =>
                {
                    b.HasOne("AppData.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AppData.Models.Cart", b =>
                {
                    b.HasOne("AppData.Models.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("AppData.Models.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AppData.Models.CartDetail", b =>
                {
                    b.HasOne("AppData.Models.Book", "Book")
                        .WithMany("CartDetails")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Cart", "Cart")
                        .WithMany("CartDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("AppData.Models.User", b =>
                {
                    b.HasOne("AppData.Models.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("AppData.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("AppData.Models.Bill", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("AppData.Models.Book", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("AppData.Models.Cart", b =>
                {
                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("AppData.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("AppData.Models.User", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Cart")
                        .IsRequired();
                });

            modelBuilder.Entity("AppData.Models.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

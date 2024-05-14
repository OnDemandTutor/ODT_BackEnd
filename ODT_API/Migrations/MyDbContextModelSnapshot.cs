﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ODT_Repository.Entity;

#nullable disable

namespace ODT_API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ODT_Repository.Entity.Bill", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("orderId")
                        .HasColumnType("bigint");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("id");

                    b.HasIndex("orderId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Blog", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("blogContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("ODT_Repository.Entity.BlogComment", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("blogComment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("blogId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("blogId");

                    b.HasIndex("userId");

                    b.ToTable("BlogComment");
                });

            modelBuilder.Entity("ODT_Repository.Entity.BlogLike", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("blogId")
                        .HasColumnType("bigint");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("totalLike")
                        .HasColumnType("int");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("blogId");

                    b.HasIndex("userId");

                    b.ToTable("BlogLike");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Category", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("money")
                        .HasColumnType("double");

                    b.Property<string>("paymentCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("studentId")
                        .HasColumnType("bigint");

                    b.Property<long>("subcriptionId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("studentId");

                    b.HasIndex("subcriptionId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Permission", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Question", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("categoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("modifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("questionContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("studentId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.HasIndex("studentId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("ODT_Repository.Entity.QuestionComment", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("modifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("questionId")
                        .HasColumnType("bigint");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("questionId");

                    b.HasIndex("userId");

                    b.ToTable("QuestionComment");
                });

            modelBuilder.Entity("ODT_Repository.Entity.QuestionRating", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("questionId")
                        .HasColumnType("bigint");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("totalRating")
                        .HasColumnType("int");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("questionId");

                    b.HasIndex("userId");

                    b.ToTable("QuestionRating");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Role", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ODT_Repository.Entity.RolePermission", b =>
                {
                    b.Property<long>("roleId")
                        .HasColumnType("bigint");

                    b.Property<long>("permissionId")
                        .HasColumnType("bigint");

                    b.HasKey("roleId", "permissionId");

                    b.HasIndex("permissionId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Student", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("ODT_Repository.Entity.StudentSubcription", b =>
                {
                    b.Property<long>("studentId")
                        .HasColumnType("bigint");

                    b.Property<long>("subcriptionId")
                        .HasColumnType("bigint");

                    b.Property<int>("currentQuestion")
                        .HasColumnType("int");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("limitQuestion")
                        .HasColumnType("int");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("studentId", "subcriptionId");

                    b.HasIndex("subcriptionId");

                    b.ToTable("StudentSubcription");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Subcription", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("subcriptionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("subcriptionPrice")
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.ToTable("Subcription");
                });

            modelBuilder.Entity("ODT_Repository.Entity.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateOnly>("dob")
                        .HasColumnType("date");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("identityCard")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("passWord")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("roleId")
                        .HasColumnType("bigint");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("roleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Wallet", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<double>("balance")
                        .HasColumnType("double");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Bill", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Blog", b =>
                {
                    b.HasOne("ODT_Repository.Entity.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("ODT_Repository.Entity.BlogComment", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Blog", "blog")
                        .WithMany()
                        .HasForeignKey("blogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("blog");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ODT_Repository.Entity.BlogLike", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Blog", "blog")
                        .WithMany()
                        .HasForeignKey("blogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("blog");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Order", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Student", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.Subcription", "subcription")
                        .WithMany()
                        .HasForeignKey("subcriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");

                    b.Navigation("subcription");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Question", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Category", "category")
                        .WithMany()
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.Student", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("student");
                });

            modelBuilder.Entity("ODT_Repository.Entity.QuestionComment", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Question", "question")
                        .WithMany()
                        .HasForeignKey("questionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("question");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ODT_Repository.Entity.QuestionRating", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Question", "question")
                        .WithMany()
                        .HasForeignKey("questionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("question");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ODT_Repository.Entity.RolePermission", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Permission", "permission")
                        .WithMany()
                        .HasForeignKey("permissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("permission");

                    b.Navigation("role");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Student", b =>
                {
                    b.HasOne("ODT_Repository.Entity.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("ODT_Repository.Entity.StudentSubcription", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Student", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ODT_Repository.Entity.Subcription", "subcription")
                        .WithMany()
                        .HasForeignKey("subcriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");

                    b.Navigation("subcription");
                });

            modelBuilder.Entity("ODT_Repository.Entity.User", b =>
                {
                    b.HasOne("ODT_Repository.Entity.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");
                });

            modelBuilder.Entity("ODT_Repository.Entity.Wallet", b =>
                {
                    b.HasOne("ODT_Repository.Entity.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}

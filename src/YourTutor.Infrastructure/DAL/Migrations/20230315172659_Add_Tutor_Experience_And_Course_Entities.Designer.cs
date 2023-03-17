﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourTutor.Infrastructure.DAL;

#nullable disable

namespace YourTutor.Infrastructure.Dal.Migrations
{
    [DbContext(typeof(YourTutorDbContext))]
    [Migration("20230315172659_Add_Tutor_Experience_And_Course_Entities")]
    partial class Add_Tutor_Experience_And_Course_Entities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YourTutor.Core.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId");

                    b.HasIndex("TutorId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Experience", b =>
                {
                    b.Property<int>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExperienceId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExperienceId");

                    b.HasIndex("TutorId");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Tutor", b =>
                {
                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TutorId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Tutor");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Course", b =>
                {
                    b.HasOne("YourTutor.Core.Entities.Tutor", null)
                        .WithMany("Courses")
                        .HasForeignKey("TutorId");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Experience", b =>
                {
                    b.HasOne("YourTutor.Core.Entities.Tutor", null)
                        .WithMany("Experiences")
                        .HasForeignKey("TutorId");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Tutor", b =>
                {
                    b.HasOne("YourTutor.Core.Entities.User", null)
                        .WithOne("Tutor")
                        .HasForeignKey("YourTutor.Core.Entities.Tutor", "UserId");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Tutor", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Experiences");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.User", b =>
                {
                    b.Navigation("Tutor");
                });
#pragma warning restore 612, 618
        }
    }
}
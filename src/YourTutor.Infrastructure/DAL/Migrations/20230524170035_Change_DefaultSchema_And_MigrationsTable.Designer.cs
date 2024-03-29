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
    [Migration("20230524170035_Change_DefaultSchema_And_MigrationsTable")]
    partial class Change_DefaultSchema_And_MigrationsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("yt")
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YourTutor.Core.Entities.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRemotely")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TutorId");

                    b.ToTable("Offers", "yt");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Tutor", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Tutor", "yt");
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

                    b.ToTable("Users", "yt");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Offer", b =>
                {
                    b.HasOne("YourTutor.Core.Entities.Tutor", "Tutor")
                        .WithMany("Offers")
                        .HasForeignKey("TutorId");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Tutor", b =>
                {
                    b.HasOne("YourTutor.Core.Entities.User", "User")
                        .WithOne("Tutor")
                        .HasForeignKey("YourTutor.Core.Entities.Tutor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.Tutor", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("YourTutor.Core.Entities.User", b =>
                {
                    b.Navigation("Tutor");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using IndividueleCSharpProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations.GamenightDB
{
    [DbContext(typeof(GamenightDBContext))]
    [Migration("20250125192129_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GamenightGames", b =>
                {
                    b.Property<int>("gameId")
                        .HasColumnType("int");

                    b.Property<int>("gameNightId")
                        .HasColumnType("int");

                    b.HasKey("gameId", "gameNightId");

                    b.HasIndex("gameNightId");

                    b.ToTable("GamenightGames");
                });

            modelBuilder.Entity("GamenightPlayers", b =>
                {
                    b.Property<int>("personId")
                        .HasColumnType("int");

                    b.Property<int>("gameNightId")
                        .HasColumnType("int");

                    b.HasKey("personId", "gameNightId");

                    b.HasIndex("gameNightId");

                    b.ToTable("GamenightPlayers");
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.GameNight", b =>
                {
                    b.Property<int>("gameNightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("gameNightId"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("alcoholic")
                        .HasColumnType("bit");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("food")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("hostId")
                        .HasColumnType("int");

                    b.Property<bool>("is18Plus")
                        .HasColumnType("bit");

                    b.Property<bool>("lactoseFree")
                        .HasColumnType("bit");

                    b.Property<int>("maxPlayers")
                        .HasColumnType("int");

                    b.Property<bool>("nutFree")
                        .HasColumnType("bit");

                    b.Property<bool>("vegetarian")
                        .HasColumnType("bit");

                    b.HasKey("gameNightId");

                    b.ToTable("GameNights");
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.Games", b =>
                {
                    b.Property<int>("gameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("gameId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("genre")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is18Plus")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("typeOfGame")
                        .HasColumnType("int");

                    b.HasKey("gameId");

                    b.ToTable("GamesDb");
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.Person", b =>
                {
                    b.Property<int>("personId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("personId"));

                    b.Property<string>("IdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("alcoholic")
                        .HasColumnType("bit");

                    b.Property<DateTime>("birthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("houseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("lactoseFree")
                        .HasColumnType("bit");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("nutFree")
                        .HasColumnType("bit");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("vegatarian")
                        .HasColumnType("bit");

                    b.HasKey("personId");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.Review", b =>
                {
                    b.Property<int>("reviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("reviewId"));

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gameNightId")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<int>("reviewerId")
                        .HasColumnType("int");

                    b.HasKey("reviewId");

                    b.HasIndex("gameNightId");

                    b.HasIndex("reviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("GamenightGames", b =>
                {
                    b.HasOne("IndividueleCSharpProject.Domain.Games", null)
                        .WithMany()
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IndividueleCSharpProject.Domain.GameNight", null)
                        .WithMany()
                        .HasForeignKey("gameNightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamenightPlayers", b =>
                {
                    b.HasOne("IndividueleCSharpProject.Domain.GameNight", null)
                        .WithMany()
                        .HasForeignKey("gameNightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IndividueleCSharpProject.Domain.Person", null)
                        .WithMany()
                        .HasForeignKey("personId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.Person", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Persons_AspNetUsers_IdentityUserId");

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.Review", b =>
                {
                    b.HasOne("IndividueleCSharpProject.Domain.GameNight", "gameNight")
                        .WithMany("reviews")
                        .HasForeignKey("gameNightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IndividueleCSharpProject.Domain.Person", "reviewer")
                        .WithMany("reviews")
                        .HasForeignKey("reviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("gameNight");

                    b.Navigation("reviewer");
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.GameNight", b =>
                {
                    b.Navigation("reviews");
                });

            modelBuilder.Entity("IndividueleCSharpProject.Domain.Person", b =>
                {
                    b.Navigation("reviews");
                });
#pragma warning restore 612, 618
        }
    }
}

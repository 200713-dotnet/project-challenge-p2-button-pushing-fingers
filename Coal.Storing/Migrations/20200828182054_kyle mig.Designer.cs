﻿// <auto-generated />
using System;
using Coal.Storing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Coal.Storing.Migrations
{
    [DbContext(typeof(CoalDbContext))]
    [Migration("20200828182054_kyle mig")]
    partial class kylemig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Coal.Storing.Models.DownloadableContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("DownloadableContents");
                });

            modelBuilder.Entity("Coal.Storing.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Coal.Storing.Models.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("Coal.Storing.Models.LibraryDLC", b =>
                {
                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.HasKey("LibraryId", "ContentId");

                    b.HasIndex("ContentId");

                    b.ToTable("LibraryDLCs");
                });

            modelBuilder.Entity("Coal.Storing.Models.LibraryGame", b =>
                {
                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.HasKey("LibraryId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("LibraryGames");
                });

            modelBuilder.Entity("Coal.Storing.Models.LibraryMod", b =>
                {
                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.Property<int>("ModId")
                        .HasColumnType("int");

                    b.HasKey("LibraryId", "ModId");

                    b.HasIndex("ModId");

                    b.ToTable("LibraryMods");
                });

            modelBuilder.Entity("Coal.Storing.Models.Mod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Mods");
                });

            modelBuilder.Entity("Coal.Storing.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Coal.Storing.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Coal.Storing.Models.DownloadableContent", b =>
                {
                    b.HasOne("Coal.Storing.Models.Game", "Game")
                        .WithMany("DownloadableContents")
                        .HasForeignKey("GameId");

                    b.HasOne("Coal.Storing.Models.Publisher", "Publisher")
                        .WithMany("DownloadableContents")
                        .HasForeignKey("PublisherId");
                });

            modelBuilder.Entity("Coal.Storing.Models.Game", b =>
                {
                    b.HasOne("Coal.Storing.Models.Publisher", "Publisher")
                        .WithMany("Games")
                        .HasForeignKey("PublisherId");
                });

            modelBuilder.Entity("Coal.Storing.Models.Library", b =>
                {
                    b.HasOne("Coal.Storing.Models.User", "User")
                        .WithOne("Library")
                        .HasForeignKey("Coal.Storing.Models.Library", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coal.Storing.Models.LibraryDLC", b =>
                {
                    b.HasOne("Coal.Storing.Models.DownloadableContent", "DownloadableContent")
                        .WithMany("LibraryDLCs")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coal.Storing.Models.Library", "Library")
                        .WithMany("LibraryDLCs")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coal.Storing.Models.LibraryGame", b =>
                {
                    b.HasOne("Coal.Storing.Models.Game", "Game")
                        .WithMany("LibraryGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coal.Storing.Models.Library", "Library")
                        .WithMany("LibraryGames")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coal.Storing.Models.LibraryMod", b =>
                {
                    b.HasOne("Coal.Storing.Models.Library", "Library")
                        .WithMany("LibraryMods")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coal.Storing.Models.Mod", "Mod")
                        .WithMany("LibraryMods")
                        .HasForeignKey("ModId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coal.Storing.Models.Mod", b =>
                {
                    b.HasOne("Coal.Storing.Models.Game", "Game")
                        .WithMany("Mods")
                        .HasForeignKey("GameId");

                    b.HasOne("Coal.Storing.Models.Publisher", "Publisher")
                        .WithMany("Mods")
                        .HasForeignKey("PublisherId");
                });
#pragma warning restore 612, 618
        }
    }
}

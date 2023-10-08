﻿// <auto-generated />
using GameService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameService.Migrations
{
    [DbContext(typeof(GameServiceContext))]
    [Migration("20231006175741_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameService.Models.Game", b =>
                {
                    b.Property<int>("gameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("gameID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("gameID");

                    b.HasIndex("GenreID");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            gameID = 1,
                            Description = "A game about heroes in action",
                            GenreID = 1,
                            Price = 10.99m,
                            Publisher = "Petroglyph",
                            ReleaseYear = 2005,
                            Title = "Heroes in Action"
                        },
                        new
                        {
                            gameID = 2,
                            Description = "A game about adventure in the forest",
                            GenreID = 2,
                            Price = 9.99m,
                            Publisher = "Inc Mania",
                            ReleaseYear = 2012,
                            Title = "Adventures in the Black Forest"
                        },
                        new
                        {
                            gameID = 3,
                            Description = "An RPG game in the city",
                            GenreID = 3,
                            Price = 8.99m,
                            Publisher = "Kronos Studios",
                            ReleaseYear = 2021,
                            Title = "Escape the City"
                        });
                });

            modelBuilder.Entity("GameService.Models.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreID"));

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreID");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreID = 1,
                            GenreName = "Action"
                        },
                        new
                        {
                            GenreID = 2,
                            GenreName = "Adventure"
                        },
                        new
                        {
                            GenreID = 3,
                            GenreName = "RPG"
                        },
                        new
                        {
                            GenreID = 4,
                            GenreName = "Simulation"
                        },
                        new
                        {
                            GenreID = 5,
                            GenreName = "Strategy"
                        },
                        new
                        {
                            GenreID = 6,
                            GenreName = "Sports"
                        },
                        new
                        {
                            GenreID = 7,
                            GenreName = "Puzzle"
                        },
                        new
                        {
                            GenreID = 8,
                            GenreName = "Idle"
                        },
                        new
                        {
                            GenreID = 9,
                            GenreName = "Casual"
                        });
                });

            modelBuilder.Entity("GameService.Models.Game", b =>
                {
                    b.HasOne("GameService.Models.Genre", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("GameService.Models.Genre", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}

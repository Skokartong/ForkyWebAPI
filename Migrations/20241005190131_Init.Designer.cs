﻿// <auto-generated />
using System;
using ForkyWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ForkyWebAPI.Migrations
{
    [DbContext(typeof(ForkyContext))]
    [Migration("20241005190131_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ForkyWebAPI.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Admin Street 1",
                            Email = "admin@forky.com",
                            FirstName = "Admin",
                            LastName = "User",
                            PasswordHash = "$2a$10$4Ur/cP.dyE1rJr.ARkVOsuXZjiGN01d1mf.CVCiXnJFUEs8cQr7de",
                            Phone = "1234567890",
                            Role = "Admin",
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Address = "User Street 5",
                            Email = "user@forky.com",
                            FirstName = "Regular",
                            LastName = "User",
                            PasswordHash = "$2a$10$jhVko2sjbNt1ekVCi6M56e.fABk8eDB6s7WlnC1WYS73LUM8mHSx6",
                            Phone = "0987654321",
                            Role = "User",
                            UserName = "user"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Maple Street 7",
                            Email = "maria@forky.com",
                            FirstName = "Maria",
                            LastName = "Svensson",
                            PasswordHash = "$2a$10$zNI2.DjrNa0AQ9bI3AbIu.igmHIcBam4X.PPflxJjCz/cMyYzOfiS",
                            Phone = "0712345678",
                            Role = "User",
                            UserName = "maria"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Pine Avenue 10",
                            Email = "karl@forky.com",
                            FirstName = "Karl",
                            LastName = "Johansson",
                            PasswordHash = "$2a$10$Le.yE1gGLFWly.kHF7AViuCFOq89/r4AHHYUIanm/3r9DNZR9Fr3.",
                            Phone = "0734567890",
                            Role = "User",
                            UserName = "karl"
                        });
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BookingStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("FK_AccountId")
                        .HasColumnType("int");

                    b.Property<int>("FK_RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("FK_TableId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FK_AccountId");

                    b.HasIndex("FK_RestaurantId");

                    b.HasIndex("FK_TableId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookingEnd = new DateTime(2024, 10, 10, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            BookingStart = new DateTime(2024, 10, 10, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            FK_AccountId = 2,
                            FK_RestaurantId = 1,
                            FK_TableId = 1,
                            Message = "Celebrating our anniversary",
                            NumberOfGuests = 2
                        },
                        new
                        {
                            Id = 2,
                            BookingEnd = new DateTime(2024, 11, 5, 21, 0, 0, 0, DateTimeKind.Unspecified),
                            BookingStart = new DateTime(2024, 11, 5, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            FK_AccountId = 3,
                            FK_RestaurantId = 2,
                            FK_TableId = 3,
                            Message = "Business dinner",
                            NumberOfGuests = 4
                        },
                        new
                        {
                            Id = 3,
                            BookingEnd = new DateTime(2024, 12, 25, 20, 30, 0, 0, DateTimeKind.Unspecified),
                            BookingStart = new DateTime(2024, 12, 25, 17, 30, 0, 0, DateTimeKind.Unspecified),
                            FK_AccountId = 4,
                            FK_RestaurantId = 4,
                            FK_TableId = 8,
                            Message = "Christmas celebration",
                            NumberOfGuests = 6
                        });
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Drink")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("FK_RestaurantId")
                        .HasColumnType("int");

                    b.Property<string>("Ingredients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("NameOfDish")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FK_RestaurantId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Drink = "Red Wine",
                            FK_RestaurantId = 1,
                            Ingredients = "Pasta, Eggs, Bacon, Cheese",
                            IsAvailable = true,
                            NameOfDish = "Pasta Carbonara",
                            Price = 12.5
                        },
                        new
                        {
                            Id = 2,
                            Drink = "White Wine",
                            FK_RestaurantId = 1,
                            Ingredients = "Romaine Lettuce, Croutons, Chicken, Parmesan",
                            IsAvailable = true,
                            NameOfDish = "Caesar Salad",
                            Price = 10.0
                        },
                        new
                        {
                            Id = 3,
                            Drink = "Champagne",
                            FK_RestaurantId = 2,
                            Ingredients = "Beef, Pastry, Mushrooms",
                            IsAvailable = true,
                            NameOfDish = "Beef Wellington",
                            Price = 25.0
                        },
                        new
                        {
                            Id = 4,
                            Drink = "Chardonnay",
                            FK_RestaurantId = 2,
                            Ingredients = "Lobster, Cream, Cheese, Mustard",
                            IsAvailable = true,
                            NameOfDish = "Lobster Thermidor",
                            Price = 30.0
                        },
                        new
                        {
                            Id = 5,
                            Drink = "Sparkling Water",
                            FK_RestaurantId = 3,
                            Ingredients = "Dough, Tomato Sauce, Mozzarella, Basil",
                            IsAvailable = true,
                            NameOfDish = "Margherita Pizza",
                            Price = 8.0
                        },
                        new
                        {
                            Id = 6,
                            Drink = "Chianti",
                            FK_RestaurantId = 3,
                            Ingredients = "Pasta Sheets, Beef, Tomato Sauce, Cheese",
                            IsAvailable = true,
                            NameOfDish = "Lasagna",
                            Price = 14.0
                        },
                        new
                        {
                            Id = 7,
                            Drink = "Sake",
                            FK_RestaurantId = 4,
                            Ingredients = "Salmon, Tuna, Yellowtail",
                            IsAvailable = true,
                            NameOfDish = "Sashimi Plate",
                            Price = 22.0
                        },
                        new
                        {
                            Id = 8,
                            Drink = "Green Tea",
                            FK_RestaurantId = 4,
                            Ingredients = "Assorted Sushi Rolls",
                            IsAvailable = true,
                            NameOfDish = "Sushi Roll Combo",
                            Price = 18.0
                        });
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TypeOfRestaurant")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdditionalInformation = "Popular for brunches",
                            Location = "City Center",
                            RestaurantName = "Forky Bistro",
                            TypeOfRestaurant = "Bistro"
                        },
                        new
                        {
                            Id = 2,
                            AdditionalInformation = "Fine dining experience",
                            Location = "Uptown",
                            RestaurantName = "Gourmet Fork",
                            TypeOfRestaurant = "Gourmet"
                        },
                        new
                        {
                            Id = 3,
                            AdditionalInformation = "Authentic Italian cuisine",
                            Location = "Downtown",
                            RestaurantName = "Italiano Delight",
                            TypeOfRestaurant = "Italian"
                        },
                        new
                        {
                            Id = 4,
                            AdditionalInformation = "Fresh sushi and sashimi",
                            Location = "Seaside",
                            RestaurantName = "Sushi Palace",
                            TypeOfRestaurant = "Japanese"
                        });
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AmountOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("FK_RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("TableNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FK_RestaurantId");

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AmountOfSeats = 4,
                            FK_RestaurantId = 1,
                            TableNumber = 1
                        },
                        new
                        {
                            Id = 2,
                            AmountOfSeats = 2,
                            FK_RestaurantId = 1,
                            TableNumber = 2
                        },
                        new
                        {
                            Id = 3,
                            AmountOfSeats = 6,
                            FK_RestaurantId = 2,
                            TableNumber = 1
                        },
                        new
                        {
                            Id = 4,
                            AmountOfSeats = 4,
                            FK_RestaurantId = 2,
                            TableNumber = 2
                        },
                        new
                        {
                            Id = 5,
                            AmountOfSeats = 4,
                            FK_RestaurantId = 3,
                            TableNumber = 1
                        },
                        new
                        {
                            Id = 6,
                            AmountOfSeats = 2,
                            FK_RestaurantId = 3,
                            TableNumber = 2
                        },
                        new
                        {
                            Id = 7,
                            AmountOfSeats = 4,
                            FK_RestaurantId = 4,
                            TableNumber = 1
                        },
                        new
                        {
                            Id = 8,
                            AmountOfSeats = 6,
                            FK_RestaurantId = 4,
                            TableNumber = 2
                        });
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Booking", b =>
                {
                    b.HasOne("ForkyWebAPI.Models.Account", "Account")
                        .WithMany("Bookings")
                        .HasForeignKey("FK_AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ForkyWebAPI.Models.Restaurant", "Restaurant")
                        .WithMany("Bookings")
                        .HasForeignKey("FK_RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ForkyWebAPI.Models.Table", "Table")
                        .WithMany("Bookings")
                        .HasForeignKey("FK_TableId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Menu", b =>
                {
                    b.HasOne("ForkyWebAPI.Models.Restaurant", "Restaurant")
                        .WithMany("Menus")
                        .HasForeignKey("FK_RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Table", b =>
                {
                    b.HasOne("ForkyWebAPI.Models.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("FK_RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Account", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Restaurant", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Menus");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("ForkyWebAPI.Models.Table", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}

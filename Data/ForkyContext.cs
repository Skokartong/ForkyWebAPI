using ForkyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace ForkyWebAPI.Data
{
    public class ForkyContext : DbContext
    {
        public ForkyContext(DbContextOptions<ForkyContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Account)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.FK_AccountId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.FK_TableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Restaurant)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.FK_RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.Menus)
                .HasForeignKey(m => m.FK_RestaurantId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Table>()
                .HasOne(t => t.Restaurant)
                .WithMany(r => r.Tables)
                .HasForeignKey(t => t.FK_RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);

            var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("AdminPassword123");
            var userPasswordHash = BCrypt.Net.BCrypt.HashPassword("UserPassword123");
            var user2PasswordHash = BCrypt.Net.BCrypt.HashPassword("User2Password456");
            var user3PasswordHash = BCrypt.Net.BCrypt.HashPassword("User3Password789");

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@forky.com",
                    UserName = "admin",
                    PasswordHash = adminPasswordHash,
                    Role = "Admin",
                    Phone = "1234567890",
                    Address = "Admin Street 1"
                },
                new Account
                {
                    Id = 2,
                    FirstName = "Regular",
                    LastName = "User",
                    Email = "user@forky.com",
                    UserName = "user",
                    PasswordHash = userPasswordHash,
                    Role = "User",
                    Phone = "0987654321",
                    Address = "User Street 5"
                },
                new Account
                {
                    Id = 3,
                    FirstName = "Maria",
                    LastName = "Svensson",
                    Email = "maria@forky.com",
                    UserName = "maria",
                    PasswordHash = user2PasswordHash,
                    Role = "User",
                    Phone = "0712345678",
                    Address = "Maple Street 7"
                },
                new Account
                {
                    Id = 4,
                    FirstName = "Karl",
                    LastName = "Johansson",
                    Email = "karl@forky.com",
                    UserName = "karl",
                    PasswordHash = user3PasswordHash,
                    Role = "User",
                    Phone = "0734567890",
                    Address = "Pine Avenue 10"
                }
            );

            // Lägg till restauranger
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant
                {
                    Id = 1,
                    RestaurantName = "Forky Bistro",
                    TypeOfRestaurant = "Bistro",
                    Location = "City Center",
                    AdditionalInformation = "Popular for brunches"
                },
                new Restaurant
                {
                    Id = 2,
                    RestaurantName = "Gourmet Fork",
                    TypeOfRestaurant = "Gourmet",
                    Location = "Uptown",
                    AdditionalInformation = "Fine dining experience"
                },
                new Restaurant
                {
                    Id = 3,
                    RestaurantName = "Italiano Delight",
                    TypeOfRestaurant = "Italian",
                    Location = "Downtown",
                    AdditionalInformation = "Authentic Italian cuisine"
                },
                new Restaurant
                {
                    Id = 4,
                    RestaurantName = "Sushi Palace",
                    TypeOfRestaurant = "Japanese",
                    Location = "Seaside",
                    AdditionalInformation = "Fresh sushi and sashimi"
                }
            );

            modelBuilder.Entity<Table>().HasData(
                // Forky Bistro
                new Table
                {
                    Id = 1,
                    TableNumber = 1,
                    AmountOfSeats = 4,
                    FK_RestaurantId = 1
                },
                new Table
                {
                    Id = 2,
                    TableNumber = 2,
                    AmountOfSeats = 2,
                    FK_RestaurantId = 1
                },
                // Gourmet Fork
                new Table
                {
                    Id = 3,
                    TableNumber = 1,
                    AmountOfSeats = 6,
                    FK_RestaurantId = 2
                },
                new Table
                {
                    Id = 4,
                    TableNumber = 2,
                    AmountOfSeats = 4,
                    FK_RestaurantId = 2
                },
                // Italiano Delight
                new Table
                {
                    Id = 5,
                    TableNumber = 1,
                    AmountOfSeats = 4,
                    FK_RestaurantId = 3
                },
                new Table
                {
                    Id = 6,
                    TableNumber = 2,
                    AmountOfSeats = 2,
                    FK_RestaurantId = 3
                },
                // Sushi Palace
                new Table
                {
                    Id = 7,
                    TableNumber = 1,
                    AmountOfSeats = 4,
                    FK_RestaurantId = 4
                },
                new Table
                {
                    Id = 8,
                    TableNumber = 2,
                    AmountOfSeats = 6,
                    FK_RestaurantId = 4
                }
            );

            modelBuilder.Entity<Menu>().HasData(
                // Forky Bistro
                new Menu
                {
                    Id = 1,
                    NameOfDish = "Pasta Carbonara",
                    Drink = "Red Wine",
                    IsAvailable = true,
                    Ingredients = "Pasta, Eggs, Bacon, Cheese",
                    Price = 12.50,
                    FK_RestaurantId = 1
                },
                new Menu
                {
                    Id = 2,
                    NameOfDish = "Caesar Salad",
                    Drink = "White Wine",
                    IsAvailable = true,
                    Ingredients = "Romaine Lettuce, Croutons, Chicken, Parmesan",
                    Price = 10.00,
                    FK_RestaurantId = 1
                },
                // Gourmet Fork
                new Menu
                {
                    Id = 3,
                    NameOfDish = "Beef Wellington",
                    Drink = "Champagne",
                    IsAvailable = true,
                    Ingredients = "Beef, Pastry, Mushrooms",
                    Price = 25.00,
                    FK_RestaurantId = 2
                },
                new Menu
                {
                    Id = 4,
                    NameOfDish = "Lobster Thermidor",
                    Drink = "Chardonnay",
                    IsAvailable = true,
                    Ingredients = "Lobster, Cream, Cheese, Mustard",
                    Price = 30.00,
                    FK_RestaurantId = 2
                },
                // Italiano Delight
                new Menu
                {
                    Id = 5,
                    NameOfDish = "Margherita Pizza",
                    Drink = "Sparkling Water",
                    IsAvailable = true,
                    Ingredients = "Dough, Tomato Sauce, Mozzarella, Basil",
                    Price = 8.00,
                    FK_RestaurantId = 3
                },
                new Menu
                {
                    Id = 6,
                    NameOfDish = "Lasagna",
                    Drink = "Chianti",
                    IsAvailable = true,
                    Ingredients = "Pasta Sheets, Beef, Tomato Sauce, Cheese",
                    Price = 14.00,
                    FK_RestaurantId = 3
                },
                // Sushi Palace
                new Menu
                {
                    Id = 7,
                    NameOfDish = "Sashimi Plate",
                    Drink = "Sake",
                    IsAvailable = true,
                    Ingredients = "Salmon, Tuna, Yellowtail",
                    Price = 22.00,
                    FK_RestaurantId = 4
                },
                new Menu
                {
                    Id = 8,
                    NameOfDish = "Sushi Roll Combo",
                    Drink = "Green Tea",
                    IsAvailable = true,
                    Ingredients = "Assorted Sushi Rolls",
                    Price = 18.00,
                    FK_RestaurantId = 4
                }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    NumberOfGuests = 2,
                    BookingStart = new DateTime(2024, 10, 10, 18, 00, 00),
                    BookingEnd = new DateTime(2024, 10, 10, 20, 00, 00),
                    Message = "Celebrating our anniversary",
                    FK_AccountId = 2, // Regular User
                    FK_TableId = 1, // Forky Bistro Table 1
                    FK_RestaurantId = 1
                },
                new Booking
                {
                    Id = 2,
                    NumberOfGuests = 4,
                    BookingStart = new DateTime(2024, 11, 5, 19, 00, 00),
                    BookingEnd = new DateTime(2024, 11, 5, 21, 00, 00),
                    Message = "Business dinner",
                    FK_AccountId = 3, // Maria Svensson
                    FK_TableId = 3, // Gourmet Fork Table 1
                    FK_RestaurantId = 2
                },
                new Booking
                {
                    Id = 3,
                    NumberOfGuests = 6,
                    BookingStart = new DateTime(2024, 12, 25, 17, 30, 00),
                    BookingEnd = new DateTime(2024, 12, 25, 20, 30, 00),
                    Message = "Christmas celebration",
                    FK_AccountId = 4,
                    FK_TableId = 8,
                    FK_RestaurantId = 4
                }
            );
        }
    }
}


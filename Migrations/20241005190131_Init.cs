using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForkyWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeOfRestaurant = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfDish = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Drink = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    FK_RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Restaurants_FK_RestaurantId",
                        column: x => x.FK_RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    AmountOfSeats = table.Column<int>(type: "int", nullable: false),
                    FK_RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_FK_RestaurantId",
                        column: x => x.FK_RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    BookingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FK_AccountId = table.Column<int>(type: "int", nullable: false),
                    FK_TableId = table.Column<int>(type: "int", nullable: false),
                    FK_RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Accounts_FK_AccountId",
                        column: x => x.FK_AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Restaurants_FK_RestaurantId",
                        column: x => x.FK_RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_FK_TableId",
                        column: x => x.FK_TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "PasswordHash", "Phone", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Admin Street 1", "admin@forky.com", "Admin", "User", "$2a$10$4Ur/cP.dyE1rJr.ARkVOsuXZjiGN01d1mf.CVCiXnJFUEs8cQr7de", "1234567890", "Admin", "admin" },
                    { 2, "User Street 5", "user@forky.com", "Regular", "User", "$2a$10$jhVko2sjbNt1ekVCi6M56e.fABk8eDB6s7WlnC1WYS73LUM8mHSx6", "0987654321", "User", "user" },
                    { 3, "Maple Street 7", "maria@forky.com", "Maria", "Svensson", "$2a$10$zNI2.DjrNa0AQ9bI3AbIu.igmHIcBam4X.PPflxJjCz/cMyYzOfiS", "0712345678", "User", "maria" },
                    { 4, "Pine Avenue 10", "karl@forky.com", "Karl", "Johansson", "$2a$10$Le.yE1gGLFWly.kHF7AViuCFOq89/r4AHHYUIanm/3r9DNZR9Fr3.", "0734567890", "User", "karl" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AdditionalInformation", "Location", "RestaurantName", "TypeOfRestaurant" },
                values: new object[,]
                {
                    { 1, "Popular for brunches", "City Center", "Forky Bistro", "Bistro" },
                    { 2, "Fine dining experience", "Uptown", "Gourmet Fork", "Gourmet" },
                    { 3, "Authentic Italian cuisine", "Downtown", "Italiano Delight", "Italian" },
                    { 4, "Fresh sushi and sashimi", "Seaside", "Sushi Palace", "Japanese" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Drink", "FK_RestaurantId", "Ingredients", "IsAvailable", "NameOfDish", "Price" },
                values: new object[,]
                {
                    { 1, "Red Wine", 1, "Pasta, Eggs, Bacon, Cheese", true, "Pasta Carbonara", 12.5 },
                    { 2, "White Wine", 1, "Romaine Lettuce, Croutons, Chicken, Parmesan", true, "Caesar Salad", 10.0 },
                    { 3, "Champagne", 2, "Beef, Pastry, Mushrooms", true, "Beef Wellington", 25.0 },
                    { 4, "Chardonnay", 2, "Lobster, Cream, Cheese, Mustard", true, "Lobster Thermidor", 30.0 },
                    { 5, "Sparkling Water", 3, "Dough, Tomato Sauce, Mozzarella, Basil", true, "Margherita Pizza", 8.0 },
                    { 6, "Chianti", 3, "Pasta Sheets, Beef, Tomato Sauce, Cheese", true, "Lasagna", 14.0 },
                    { 7, "Sake", 4, "Salmon, Tuna, Yellowtail", true, "Sashimi Plate", 22.0 },
                    { 8, "Green Tea", 4, "Assorted Sushi Rolls", true, "Sushi Roll Combo", 18.0 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "AmountOfSeats", "FK_RestaurantId", "TableNumber" },
                values: new object[,]
                {
                    { 1, 4, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 6, 2, 1 },
                    { 4, 4, 2, 2 },
                    { 5, 4, 3, 1 },
                    { 6, 2, 3, 2 },
                    { 7, 4, 4, 1 },
                    { 8, 6, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingEnd", "BookingStart", "FK_AccountId", "FK_RestaurantId", "FK_TableId", "Message", "NumberOfGuests" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 10, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 1, "Celebrating our anniversary", 2 },
                    { 2, new DateTime(2024, 11, 5, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 19, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 3, "Business dinner", 4 },
                    { 3, new DateTime(2024, 12, 25, 20, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 25, 17, 30, 0, 0, DateTimeKind.Unspecified), 4, 4, 8, "Christmas celebration", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_AccountId",
                table: "Bookings",
                column: "FK_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_RestaurantId",
                table: "Bookings",
                column: "FK_RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_TableId",
                table: "Bookings",
                column: "FK_TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_FK_RestaurantId",
                table: "Menus",
                column: "FK_RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_FK_RestaurantId",
                table: "Tables",
                column: "FK_RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}

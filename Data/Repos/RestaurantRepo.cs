using ForkyWebAPI.Models;
using ForkyWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkyWebAPI.Data.Repos.IRepos;
using Microsoft.Identity.Client;

namespace ForkyWebAPI.Data.Repos
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly ForkyContext _context;

        public RestaurantRepo(ForkyContext context)
        {
            _context = context;
        }

        //GET BY ID
        public async Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId)
        {
            return await _context.Restaurants
                .FirstOrDefaultAsync(a => a.Id == restaurantId);
        }

        public async Task<Menu?> GetDishByIdAsync(int menuId)
        {
            return await _context.Menus
                .FirstOrDefaultAsync(a => a.Id == menuId);
        }

        public async Task<Table?> GetTableByIdAsync(int tableId)
        {
            return await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == tableId);
        }

        public async Task<IEnumerable<Table?>> GetTablesByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Tables.Where(t => t.FK_RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<bool> GetAvailableMenuItemAsync(int menuId)
        {
            var menuItem = await _context.Menus.FindAsync(menuId);
            return menuItem?.IsAvailable ?? false;
        }

        // ADD OBJECT
        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task AddTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task AddDishOrDrinkAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        // DELETE OBJECT
        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(Table table)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDishOrDrinkAsync(Menu menu)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }

        // UPDATE OBJECT
        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync(Restaurant updatedRestaurant)
        {
            _context.Restaurants.Update(updatedRestaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDishOrDrinkAsync(Menu updatedMenu)
        {
            _context.Menus.Update(updatedMenu);
            await _context.SaveChangesAsync();
        }

        // GET ALL OBJECTS
        public async Task<IEnumerable<Restaurant?>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<IEnumerable<Table?>> GetAllTablesAsync()
        {
            var tables = _context.Tables.Include(t => t.Restaurant).Select(t => new
            {
                t.Id,
                t.Restaurant.RestaurantName,
                t.TableNumber,
                t.AmountOfSeats,
                t.FK_RestaurantId,
            });

            return await _context.Tables.ToListAsync();
        }

        public async Task<IEnumerable<Menu?>> GetAllMenusAsync()
        {
            var menus = _context.Menus.Include(m => m.Restaurant).Select(m => new
            {
                m.Id,
                m.Restaurant.RestaurantName,
                m.NameOfDish,
                m.Drink,
                m.IsAvailable,
                m.Ingredients,
                m.Price,
                m.FK_RestaurantId
            });

            return await _context.Menus.ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(int restaurantId, DateTime startTime, DateTime endTime, int numberOfGuests, int? bookingId)
        {
            var query = _context.Tables
                .Where(t => t.FK_RestaurantId == restaurantId && t.AmountOfSeats >= numberOfGuests);

            var conflictingBookings = await _context.Bookings
                .Where(b => b.FK_RestaurantId == restaurantId &&
                            b.BookingStart < endTime && 
                            b.BookingEnd > startTime && 
                            (!bookingId.HasValue || b.Id != bookingId)) 
                .Select(b => b.FK_TableId)
                .ToListAsync();

            query = query.Where(t => !conflictingBookings.Contains(t.Id));

            var availableTables = await query.ToListAsync();

            if (bookingId.HasValue)
            {
                var currentBooking = await _context.Bookings.FindAsync(bookingId.Value);
                if (currentBooking != null && currentBooking.FK_TableId != null)
                {
                    var currentTable = await _context.Tables.FindAsync(currentBooking.FK_TableId);
                    if (currentTable != null && currentTable.AmountOfSeats >= numberOfGuests)
                    {
                        availableTables.Add(currentTable);
                    }
                }
            }

            return availableTables.Distinct();
        }


        public async Task<IEnumerable<Menu?>> GetMenuAsync(int restaurantId)
        {
            return await _context.Menus
                .Where(m => m.FK_RestaurantId == restaurantId)
                .ToListAsync();
        }
    }
}


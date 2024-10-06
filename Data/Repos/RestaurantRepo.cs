using ForkyWebAPI.Models;
using ForkyWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkyWebAPI.Data.Repos.IRepos;

namespace ForkyWebAPI.Data.Repos
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly ForkyContext _context;

        public RestaurantRepo(ForkyContext context)
        {
            _context = context;
        }

        public async Task AddRestaurantAsync(Models.Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateRestaurantAsync(int restaurantId, Models.Restaurant updatedRestaurant)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                restaurant.RestaurantName = updatedRestaurant.RestaurantName;
                restaurant.TypeOfRestaurant = updatedRestaurant.TypeOfRestaurant;
                restaurant.AdditionalInformation = updatedRestaurant.AdditionalInformation;
                restaurant.Location = updatedRestaurant.Location;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Restaurant?> SearchRestaurantAsync(int restaurantId)
        {
            return await _context.Restaurants.FindAsync(restaurantId);
        }

        public async Task<IEnumerable<Restaurant?>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Tables.Where(t => t.FK_RestaurantId == restaurantId).ToListAsync();
        }

        public async Task AddTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTableAsync(int tableId, Table updatedTable)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                table.TableNumber = updatedTable.TableNumber;
                table.AmountOfSeats = updatedTable.AmountOfSeats;
                table.FK_RestaurantId = updatedTable.FK_RestaurantId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(int restaurantId, DateTime startTime, DateTime endTime, int numberOfGuests)
        {
                return await _context.Tables
        .Where(t => t.FK_RestaurantId == restaurantId &&
                    t.AmountOfSeats >= numberOfGuests &&
                    !t.Bookings.Any(r => r.BookingStart < endTime && r.BookingEnd > startTime))
        .ToListAsync();
        }

        public async Task AddDishOrDrinkAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDishOrDrinkAsync(int menuId)
        {
            var menuItem = await _context.Menus.FindAsync(menuId);
            if (menuItem != null)
            {
                _context.Menus.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateDishOrDrinkAsync(int menuId, Menu updateMenu)
        {
            var menuItem = await _context.Menus.FindAsync(menuId);
            if (menuItem != null)
            {
                menuItem.NameOfDish = updateMenu.NameOfDish;
                menuItem.Drink = updateMenu.Drink;
                menuItem.IsAvailable = updateMenu.IsAvailable;
                menuItem.Ingredients = updateMenu.Ingredients;
                menuItem.Price = updateMenu.Price;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Menu?>> SeeMenuAsync(int restaurantId)
        {
            return await _context.Menus
                .Where(m => m.FK_RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<bool> GetAvailableMenuItemAsync(int menuId)
        {
            var menuItem = await _context.Menus.FindAsync(menuId);
            return menuItem?.IsAvailable ?? false;
        }
    }
}


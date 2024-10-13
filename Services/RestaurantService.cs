using ForkyWebAPI.Models;
using ForkyWebAPI.Models.RestaurantDTOs;
using ForkyWebAPI.Data.Repos.IRepos;
using ForkyWebAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkyWebAPI.Models.MenuDTOs;
using ForkyWebAPI.Models.TableDTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ForkyWebAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepo _restaurantRepo;

        public RestaurantService(IRestaurantRepo restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

        // ADD ACTIONS FOR RESTAURANTS, MENUS
        public async Task AddRestaurantAsync(AddRestaurantDTO addRestaurantDTO)
        {
            if (addRestaurantDTO == null)
            {
                throw new ArgumentNullException(nameof(addRestaurantDTO), "Restaurant cannot be null.");
            }

            var restaurant = new Restaurant
            {
                RestaurantName = addRestaurantDTO.RestaurantName,
                TypeOfRestaurant = addRestaurantDTO.TypeOfRestaurant,
                Location = addRestaurantDTO.Location,
                AdditionalInformation = addRestaurantDTO.AdditionalInformation
            };

            await _restaurantRepo.AddRestaurantAsync(restaurant);
        }

        public async Task AddDishOrDrinkAsync(AddDishDTO dishDTO)
        {
            if (dishDTO == null)
            {
                throw new ArgumentNullException(nameof(dishDTO), "Menu cannot be null.");
            }

            var menu = new Menu
            {
                NameOfDish = dishDTO.NameOfDish,
                Drink = dishDTO.Drink,
                IsAvailable = dishDTO.IsAvailable,
                Ingredients = dishDTO.Ingredients,
                Price = dishDTO.Price,
                FK_RestaurantId = dishDTO.FK_RestaurantId
            };

            await _restaurantRepo.AddDishOrDrinkAsync(menu);
        }

        public async Task AddTableAsync(NewTableDTO newTableDTO)
        {
            if (newTableDTO == null)
            {
                throw new ArgumentNullException(nameof(newTableDTO), "Table cannot be null.");
            }

            var table = new Models.Table
            {
                TableNumber = newTableDTO.TableNumber,
                AmountOfSeats = newTableDTO.AmountOfSeats,
                FK_RestaurantId = newTableDTO.FK_RestaurantId,
            };

            await _restaurantRepo.AddTableAsync(table);
        }


        // UPDATE ACTIONS 
        public async Task UpdateRestaurantAsync(int restaurantId, UpdateRestaurantDTO updateRestaurantDTO)
        {
            if (updateRestaurantDTO == null)
            {
                throw new ArgumentNullException(nameof(updateRestaurantDTO), "Updated restaurant cannot be null.");
            }

            var existingRestaurant = await _restaurantRepo.GetRestaurantByIdAsync(restaurantId);

            if (existingRestaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            existingRestaurant.RestaurantName = updateRestaurantDTO.RestaurantName;
            existingRestaurant.TypeOfRestaurant = updateRestaurantDTO.TypeOfRestaurant;
            existingRestaurant.Location = updateRestaurantDTO.Location;
            existingRestaurant.AdditionalInformation = updateRestaurantDTO.AdditionalInformation;

            await _restaurantRepo.UpdateRestaurantAsync(existingRestaurant);
        }


        public async Task UpdateDishOrDrinkAsync(int menuId, UpdateMenuDTO updateMenuDTO)
        {
            if (updateMenuDTO == null)
            {
                throw new ArgumentNullException(nameof(updateMenuDTO), "Updated menu item cannot be null.");
            }

            var existingMenuItem = await _restaurantRepo.GetDishByIdAsync(menuId);
            if (existingMenuItem == null)
            {
                throw new KeyNotFoundException("Menu item not found.");
            }

            existingMenuItem.NameOfDish = updateMenuDTO.NameOfDish;
            existingMenuItem.Drink = updateMenuDTO.Drink;
            existingMenuItem.Ingredients = updateMenuDTO.Ingredients;
            existingMenuItem.IsAvailable = updateMenuDTO.IsAvailable;
            existingMenuItem.Price = updateMenuDTO.Price;
            existingMenuItem.FK_RestaurantId = updateMenuDTO.FK_RestaurantId;

            await _restaurantRepo.UpdateDishOrDrinkAsync(existingMenuItem);
        }

        public async Task UpdateTableAsync(int tableId, UpdateTableDTO updateTableDTO)
        {
            if (updateTableDTO == null)
            {
                throw new ArgumentNullException(nameof(updateTableDTO), "Updated table cannot be null");
            }

            var existingTable = await _restaurantRepo.GetTableByIdAsync(tableId);
            if (existingTable == null)
            {
                throw new KeyNotFoundException("Table not found");
            }

            existingTable.TableNumber = updateTableDTO.TableNumber;
            existingTable.AmountOfSeats = updateTableDTO.AmountOfSeats;
            existingTable.FK_RestaurantId = updateTableDTO.FK_RestaurantId;

            await _restaurantRepo.UpdateTableAsync(existingTable);
        }

        // DELETE ACTIONS
        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var existingRestaurant = await _restaurantRepo.GetRestaurantByIdAsync(restaurantId);
            if (existingRestaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            await _restaurantRepo.DeleteRestaurantAsync(existingRestaurant);
        }

        public async Task DeleteDishOrDrinkAsync(int menuId)
        {
            var existingDish = await _restaurantRepo.GetDishByIdAsync(menuId);
            if (existingDish == null)
            {
                throw new KeyNotFoundException("Menu item not found or is unavailable.");
            }

            await _restaurantRepo.DeleteDishOrDrinkAsync(existingDish);
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var existingTable = await _restaurantRepo.GetTableByIdAsync(tableId);
            if (existingTable == null)
            {
                throw new KeyNotFoundException("Table not found");
            }

            await _restaurantRepo.DeleteTableAsync(existingTable);
        }

        // GET SINGULAR 
        public async Task<ViewRestaurantDTO?> GetRestaurantByIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantRepo.GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            var restaurantDTO = new ViewRestaurantDTO
            {
                Id = restaurantId,
                RestaurantName = restaurant.RestaurantName,
                TypeOfRestaurant = restaurant.TypeOfRestaurant,
                Location = restaurant.Location,
                AdditionalInformation = restaurant.AdditionalInformation,
            };

            return restaurantDTO;
        }

        public async Task<ViewMenuDTO?> GetDishByIdAsync(int menuId)
        {
            var menuItem = await _restaurantRepo.GetDishByIdAsync(menuId);
            if (menuItem == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            var menuDTO = new ViewMenuDTO
            {
                Id = menuId,
                NameOfDish = menuItem.NameOfDish,
                Drink = menuItem.Drink,
                IsAvailable = menuItem.IsAvailable,
                Ingredients = menuItem.Ingredients,
                FK_RestaurantId = menuItem.FK_RestaurantId,
                Price = menuItem.Price
            };

            return menuDTO;
        }

        public async Task<ViewTableDTO?> GetTableByIdAsync(int tableId)
        {
            var table = await _restaurantRepo.GetTableByIdAsync(tableId);
            if (table == null)
            {
                throw new KeyNotFoundException("Table not found.");
            }

            var tableDTO = new ViewTableDTO
            {
                Id = tableId,
                AmountOfSeats = table.AmountOfSeats,
                FK_RestaurantId = table.FK_RestaurantId,
                TableNumber = table.TableNumber
            };

            return tableDTO;
        }

        // GET PLURAL
        public async Task<IEnumerable<ViewRestaurantDTO?>> GetAllRestaurantsAsync()
        {
            var restaurants = await _restaurantRepo.GetAllRestaurantsAsync();
            var restaurantDTOs = new List<ViewRestaurantDTO>();

            foreach (var restaurant in restaurants)
            {
                restaurantDTOs.Add(new ViewRestaurantDTO
                {
                    Id = restaurant.Id,
                    RestaurantName = restaurant.RestaurantName,
                    TypeOfRestaurant = restaurant.TypeOfRestaurant,
                    Location = restaurant.Location,
                    AdditionalInformation = restaurant.AdditionalInformation
                });
            }

            return restaurantDTOs;
        }

        public async Task<IEnumerable<ViewTableDTO?>> GetAllTablesAsync()
        {
            var tables = await _restaurantRepo.GetAllTablesAsync();
            var tableDTOs = new List<ViewTableDTO>();

            foreach (var table in tables)
            {
                tableDTOs.Add(new ViewTableDTO
                {
                    Id = table.Id,
                    TableNumber = table.TableNumber,
                    AmountOfSeats = table.AmountOfSeats,
                    FK_RestaurantId = table.FK_RestaurantId,
                });
            }

            return tableDTOs;
        }

        public async Task<IEnumerable<ViewMenuDTO?>> GetAllMenusAsync()
        {
            var menus = await _restaurantRepo.GetAllMenusAsync();
            var menuDTOs = new List<ViewMenuDTO>();

            foreach (var menu in menus)
            {
                menuDTOs.Add(new ViewMenuDTO
                {
                    Id = menu.Id,
                    NameOfDish = menu.NameOfDish,
                    Drink = menu.Drink,
                    Price = menu.Price,
                    Ingredients = menu.Ingredients,
                    IsAvailable = menu.IsAvailable,
                    FK_RestaurantId = menu.FK_RestaurantId
                });
            }

            return menuDTOs;
        }

        // GET PLURAL (SPECIFIC), E.G FILTER MENU BASED ON RESTAURANT
        public async Task<IEnumerable<ViewMenuDTO?>> GetMenuByRestaurantIdAsync(int restaurantId)
        {
            var menus = await _restaurantRepo.GetMenuAsync(restaurantId);
            var menuDTOs = new List<ViewMenuDTO>();

            foreach (var menu in menus)
            {
                menuDTOs.Add(new ViewMenuDTO
                {
                    Id = menu.Id,
                    NameOfDish = menu.NameOfDish,
                    Drink = menu.Drink,
                    IsAvailable = menu.IsAvailable,
                    Ingredients = menu.Ingredients,
                    Price = menu.Price,
                    FK_RestaurantId = menu.FK_RestaurantId,
                });
            }

            return menuDTOs;
        }

        public async Task<IEnumerable<ViewTableDTO?>> GetTablesByRestaurantIdAsync(int restaurantId)
        {
            var tables = await _restaurantRepo.GetTablesByRestaurantIdAsync(restaurantId);

            return tables.Select(t => new ViewTableDTO
            {
                Id = t.Id,
                TableNumber = t.TableNumber,
                AmountOfSeats = t.AmountOfSeats,
                FK_RestaurantId = t.FK_RestaurantId
            }).ToList();
        }
    }
}


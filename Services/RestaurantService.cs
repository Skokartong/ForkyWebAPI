using ForkyWebAPI.Models;
using ForkyWebAPI.Models.RestaurantDTOs;
using ForkyWebAPI.Data.Repos.IRepos;
using ForkyWebAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ForkyWebAPI.Models.MenuDTOs;
using ForkyWebAPI.Models.TableDTOs;

namespace ForkyWebAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepo _restaurantRepo;

        public RestaurantService(IRestaurantRepo restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

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

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var restaurant = await _restaurantRepo.SearchRestaurantAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            await _restaurantRepo.DeleteRestaurantAsync(restaurantId);
        }

        public async Task UpdateRestaurantAsync(int restaurantId, UpdateRestaurantDTO updateRestaurantDTO)
        {
            if (updateRestaurantDTO == null)
            {
                throw new ArgumentNullException(nameof(updateRestaurantDTO), "Updated restaurant cannot be null.");
            }

            var restaurant = await _restaurantRepo.SearchRestaurantAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            restaurantId = updateRestaurantDTO.Id;
            restaurant.RestaurantName = updateRestaurantDTO.RestaurantName;
            restaurant.TypeOfRestaurant = updateRestaurantDTO.TypeOfRestaurant;
            restaurant.Location = updateRestaurantDTO.Location;
            restaurant.AdditionalInformation = updateRestaurantDTO.AdditionalInformation;

            await _restaurantRepo.UpdateRestaurantAsync(restaurantId, restaurant);
        }

        public async Task<ViewRestaurantDTO?> GetRestaurantByIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantRepo.SearchRestaurantAsync(restaurantId);
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

        public async Task<IEnumerable<ViewMenuDTO?>> SeeMenuAsync(int restaurantId)
        {
            var menus = await _restaurantRepo.SeeMenuAsync(restaurantId);
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
                });
            }

            return menuDTOs;
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

        public async Task DeleteDishOrDrinkAsync(int menuId)
        {
            var menuItemExists = await _restaurantRepo.GetAvailableMenuItemAsync(menuId);
            if (!menuItemExists)
            {
                throw new KeyNotFoundException("Menu item not found or is unavailable.");
            }

            await _restaurantRepo.DeleteDishOrDrinkAsync(menuId);
        }

        public async Task UpdateDishOrDrinkAsync(int menuId, UpdateMenuDTO updateMenuDTO)
        {
            if (updateMenuDTO == null)
            {
                throw new ArgumentNullException(nameof(updateMenuDTO), "Updated menu item cannot be null.");
            }

            var existingMenuItem = await _restaurantRepo.SeeMenuAsync(menuId);
            if (existingMenuItem == null)
            {
                throw new KeyNotFoundException("Menu item not found.");
            }

            var updatedMenuItem = new Menu
            {
                Id = menuId,
                NameOfDish = updateMenuDTO.NameOfDish,
                Drink = updateMenuDTO.Drink,
                Ingredients = updateMenuDTO.Ingredients,
                IsAvailable = updateMenuDTO.IsAvailable,
                Price = updateMenuDTO.Price, 
                FK_RestaurantId = updateMenuDTO.FK_RestaurantId 
            };

            await _restaurantRepo.UpdateDishOrDrinkAsync(menuId, updatedMenuItem);
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
                    IsAvailable = menu.IsAvailable
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

        public async Task AddTableAsync(NewTableDTO newTableDTO)
        {
            var table = new Table
            {
                TableNumber = newTableDTO.TableNumber,
                AmountOfSeats = newTableDTO.AmountOfSeats,
                FK_RestaurantId = newTableDTO.FK_RestaurantId,
            };

            await _restaurantRepo.AddTableAsync(table);
        }

        public async Task DeleteTableAsync(int tableId)
        {
            await _restaurantRepo.DeleteTableAsync(tableId);
        }

        public async Task UpdateTableAsync(int tableId, UpdateTableDTO updateTableDTO)
        {
            var table = new Table
            {
                Id = tableId,
                TableNumber = updateTableDTO.TableNumber,
                AmountOfSeats = updateTableDTO.AmountOfSeats,
                FK_RestaurantId = updateTableDTO.FK_RestaurantId,
            };

            await _restaurantRepo.UpdateTableAsync(tableId, table);
        }
    }
}


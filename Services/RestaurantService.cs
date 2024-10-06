using ForkyWebAPI.Models;
using ForkyWebAPI.Models.RestaurantDTOs;
using ForkyWebAPI.Data.Repos.IRepos;
using ForkyWebAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForkyWebAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepo _restaurantRepo;

        public RestaurantService(IRestaurantRepo restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

        public async Task AddRestaurantAsync(RestaurantDTO restaurantDTO)
        {
            if (restaurantDTO == null)
            {
                throw new ArgumentNullException(nameof(restaurantDTO), "Restaurant cannot be null.");
            }

            var restaurant = new Restaurant
            {
                RestaurantName = restaurantDTO.RestaurantName,
                TypeOfRestaurant = restaurantDTO.TypeOfRestaurant,
                Location = restaurantDTO.Location,
                AdditionalInformation = restaurantDTO.AdditionalInformation
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

        public async Task UpdateRestaurantAsync(int restaurantId, RestaurantDTO updatedRestaurantDTO)
        {
            if (updatedRestaurantDTO == null)
            {
                throw new ArgumentNullException(nameof(updatedRestaurantDTO), "Updated restaurant cannot be null.");
            }

            var restaurant = await _restaurantRepo.SearchRestaurantAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            restaurantId = updatedRestaurantDTO.Id;
            restaurant.RestaurantName = updatedRestaurantDTO.RestaurantName;
            restaurant.TypeOfRestaurant = updatedRestaurantDTO.TypeOfRestaurant;
            restaurant.Location = updatedRestaurantDTO.Location;
            restaurant.AdditionalInformation = updatedRestaurantDTO.AdditionalInformation;

            await _restaurantRepo.UpdateRestaurantAsync(restaurantId, restaurant);
        }

        public async Task<RestaurantDTO> GetRestaurantByIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantRepo.SearchRestaurantAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            var restaurantDTO = new RestaurantDTO
            {
                Id = restaurantId,
                RestaurantName = restaurant.RestaurantName,
                TypeOfRestaurant = restaurant.TypeOfRestaurant,
                Location = restaurant.Location,
                AdditionalInformation = restaurant.AdditionalInformation
            };

            return restaurantDTO;
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync()
        {
            var restaurants = await _restaurantRepo.GetAllRestaurantsAsync();
            var restaurantDTOs = new List<RestaurantDTO>();

            foreach (var restaurant in restaurants)
            {
                restaurantDTOs.Add(new RestaurantDTO
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

        public async Task<IEnumerable<MenuDTO>> SeeMenuAsync(int restaurantId)
        {
            var menus = await _restaurantRepo.SeeMenuAsync(restaurantId);
            var menuDTOs = new List<MenuDTO>();

            foreach (var menu in menus)
            {
                menuDTOs.Add(new MenuDTO
                {
                    NameOfDish = menu.NameOfDish,
                    Drink = menu.Drink,
                    IsAvailable = menu.IsAvailable,
                    Ingredients = menu.Ingredients,
                    Price = menu.Price,
                    FK_RestaurantId = menu.FK_RestaurantId
                });
            }

            return menuDTOs;
        }

        public async Task AddDishOrDrinkAsync(MenuDTO menuDTO)
        {
            if (menuDTO == null)
            {
                throw new ArgumentNullException(nameof(menuDTO), "Menu cannot be null.");
            }

            var menu = new Menu
            {
                NameOfDish = menuDTO.NameOfDish,
                Drink = menuDTO.Drink,
                IsAvailable = menuDTO.IsAvailable,
                Ingredients = menuDTO.Ingredients,
                Price = menuDTO.Price,
                FK_RestaurantId = menuDTO.FK_RestaurantId
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

        public async Task UpdateDishOrDrinkAsync(int menuId, MenuDTO updateMenuDTO)
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

        public async Task<IEnumerable<TableDTO>> GetTablesByRestaurantIdAsync(int restaurantId)
        {
            var tables = await _restaurantRepo.GetTablesByRestaurantIdAsync(restaurantId);

            return tables.Select(t => new TableDTO
            {
                Id = t.Id,
                TableNumber = t.TableNumber,
                AmountOfSeats = t.AmountOfSeats,
                FK_RestaurantId = t.FK_RestaurantId
            }).ToList();
        }

        public async Task AddTableAsync(TableDTO tableDTO)
        {
            var table = new Table
            {
                Id = tableDTO.Id,
                TableNumber = tableDTO.TableNumber,
                AmountOfSeats = tableDTO.AmountOfSeats,
                FK_RestaurantId = tableDTO.FK_RestaurantId,
            };

            await _restaurantRepo.AddTableAsync(table);
        }

        public async Task DeleteTableAsync(int tableId)
        {
            await _restaurantRepo.DeleteTableAsync(tableId);
        }

        public async Task UpdateTableAsync(int tableId, TableDTO tableDTO)
        {
            var table = new Table
            {
                Id = tableId,
                TableNumber = tableDTO.TableNumber,
                AmountOfSeats = tableDTO.AmountOfSeats,
                FK_RestaurantId = tableDTO.FK_RestaurantId,
            };

            await _restaurantRepo.UpdateTableAsync(tableId, table);
        }
    }
}


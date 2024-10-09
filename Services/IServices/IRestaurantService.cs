using ForkyWebAPI.Models;
using ForkyWebAPI.Models.MenuDTOs;
using ForkyWebAPI.Models.RestaurantDTOs;
using ForkyWebAPI.Models.TableDTOs;

namespace ForkyWebAPI.Services.IServices
{
    public interface IRestaurantService
    {
        Task AddRestaurantAsync(AddRestaurantDTO addRestaurantDTO);
        Task DeleteRestaurantAsync(int restaurantId);
        Task UpdateRestaurantAsync(int restaurantId, UpdateRestaurantDTO updateRestaurantDTO);

        Task AddTableAsync(NewTableDTO newTableDTO);
        Task DeleteTableAsync(int tableId);
        Task UpdateTableAsync(int tableId, UpdateTableDTO updateTableDTO);

        Task AddDishOrDrinkAsync(AddDishDTO dishDTO);
        Task DeleteDishOrDrinkAsync(int menuId);
        Task UpdateDishOrDrinkAsync(int menuId, UpdateMenuDTO updateMenuDTO);

        Task<ViewRestaurantDTO?> GetRestaurantByIdAsync(int restaurantId);
        Task<ViewMenuDTO?> GetDishByIdAsync(int menuId);
        Task<ViewTableDTO?> GetTableByIdAsync(int tableId);

        Task<IEnumerable<ViewTableDTO?>> GetTablesByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<ViewMenuDTO?>> GetMenuByRestaurantIdAsync(int restaurantId);

        Task<IEnumerable<ViewRestaurantDTO?>> GetAllRestaurantsAsync();
        Task<IEnumerable<ViewMenuDTO?>> GetAllMenusAsync();
        Task<IEnumerable<ViewTableDTO?>> GetAllTablesAsync();
    }
}

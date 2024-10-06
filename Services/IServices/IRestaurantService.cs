using ForkyWebAPI.Models.RestaurantDTOs;

namespace ForkyWebAPI.Services.IServices
{
    public interface IRestaurantService
    {
        Task AddRestaurantAsync(RestaurantDTO restaurantDTO);
        Task DeleteRestaurantAsync(int restaurantId);
        Task UpdateRestaurantAsync(int restaurantId, RestaurantDTO updatedRestaurantDTO);
        Task<RestaurantDTO> GetRestaurantByIdAsync(int restaurantId);
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync();
        Task<IEnumerable<MenuDTO>> SeeMenuAsync(int restaurantId);
        Task AddDishOrDrinkAsync(MenuDTO menuDTO);
        Task DeleteDishOrDrinkAsync(int menuId);
        Task UpdateDishOrDrinkAsync(int menuId, MenuDTO updateMenuDTO);
        Task<IEnumerable<TableDTO>> GetTablesByRestaurantIdAsync(int restaurantId);
        Task AddTableAsync(TableDTO tableDTO);
        Task DeleteTableAsync(int tableId);
        Task UpdateTableAsync(int tableId, TableDTO tableDTO);
    }
}

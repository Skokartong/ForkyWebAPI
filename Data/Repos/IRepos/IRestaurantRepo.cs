using ForkyWebAPI.Models;
using ForkyWebAPI.Models.RestaurantDTOs;

namespace ForkyWebAPI.Data.Repos.IRepos
{
    public interface IRestaurantRepo
    {
        // ADD, DELETE, UPDATE RESTAURANT
        Task AddRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(Restaurant restaurant);
        Task UpdateRestaurantAsync(Restaurant updatedRestaurant);

        // GET SEPARATE OBJECT ACTIONS
        Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId);
        Task<Menu?> GetDishByIdAsync(int menuId);
        Task<Table?> GetTableByIdAsync(int tableId);

        // GET ALL OBJECTS ACTIONS
        Task<IEnumerable<Restaurant?>> GetAllRestaurantsAsync();
        Task<IEnumerable<Table?>> GetAllTablesAsync();
        Task<IEnumerable<Menu?>> GetAllMenusAsync();
        Task<IEnumerable<Table?>> GetTablesByRestaurantIdAsync(int restaurantId);

        // ADD, DELETE, UPDATE AND AVAILABILITY FOR TABLES
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(Table table);
        Task UpdateTableAsync(Table updatedTable);
        Task<IEnumerable<Table?>> GetAvailableTablesAsync(int restaurantId, DateTime startTime, DateTime endTime, int numberOfGuests);
        
        // ADD, DELETE, UPDATE MENU
        Task AddDishOrDrinkAsync(Menu menu);
        Task DeleteDishOrDrinkAsync(Menu menu);
        Task UpdateDishOrDrinkAsync(Menu updatedMenu);

        Task<IEnumerable<Menu?>> GetMenuAsync(int FK_RestaurantId);
        Task<bool> GetAvailableMenuItemAsync(int menuId);
    }
}
